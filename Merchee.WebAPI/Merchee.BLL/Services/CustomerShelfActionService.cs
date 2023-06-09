using AutoMapper;
using FluentResults;
using MediatR;
using Merchee.BLL.Abstractions;
using Merchee.BLL.Errors;
using Merchee.BLL.MediatR.Messages;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Merchee.BLL.Services
{
    public class CustomerShelfActionService : BaseCompanyEntityService<CustomerShelfAction>, ICustomerShelfActionService
    {
        private readonly IMediator _mediator;

        public CustomerShelfActionService(CompanyDbContext dbContext, IMapper mapper, IMediator mediator) : base(dbContext, mapper)
        {
            _mediator = mediator;
        }

        public async Task<Result<Guid>> AddAsync(CustomerShelfAction customerShelfAction, string accessToken)
        {
            var shelfProduct = await _dbContext.Set<ShelfProduct>()
                .Include(e => e.Product)
                .Include(e => e.Shelf)
                .FirstOrDefaultAsync(e => e.Id == customerShelfAction.ShelfProductId);
            if (shelfProduct is null)
                return Result.Fail(new NotFoundError());

            if (accessToken != shelfProduct.Shelf.AccessToken)
                return Result.Fail(new UnauthorizedError());

            if (!shelfProduct.Active)
                return Result.Fail(new InactiveItemError());

            var companyId = shelfProduct.CompanyId;

            customerShelfAction.Id = Guid.NewGuid();
            customerShelfAction.Time = DateTime.UtcNow;
            customerShelfAction.Price = shelfProduct.Product.Price;
            customerShelfAction.CompanyId = companyId;
            customerShelfAction.Active = true;

            if (customerShelfAction.Type == Domain.Enums.CustomerShelfActionType.Take)
            {
                var newShelfProductQuantity = shelfProduct.CurrentQuantity - customerShelfAction.Quantity;
                shelfProduct.CurrentQuantity = newShelfProductQuantity >= 0 ? newShelfProductQuantity : 0;

                var replanishmentRequest = await _dbContext.Set<ReplenishmentRequest>()
                    .FirstOrDefaultAsync(e => e.ShelfProductId == customerShelfAction.ShelfProductId && !e.IsCompleted);
                if (replanishmentRequest is not null)
                {
                    if (shelfProduct.CurrentQuantity > 0)
                    {
                        replanishmentRequest.QuantityNeeded += customerShelfAction.Quantity;

                        await _mediator.Publish(new ReplenishmentRequestUpdated()
                        {
                            Barcode = shelfProduct.Product.Barcode,
                            ProductName = shelfProduct.Product.Name,
                            NewQuantityNeeded = replanishmentRequest.QuantityNeeded,
                            CompanyId = companyId
                        });
                    }
                }
                else if (shelfProduct.CurrentQuantity < shelfProduct.MinQuantity)
                {
                    var newReplenishmentRequest = new ReplenishmentRequest()
                    {
                        CompanyId = companyId,
                        Active = true,
                        Id = Guid.NewGuid(),
                        IsCompleted = false,
                        QuantityNeeded = shelfProduct.FullCapacity - shelfProduct.CurrentQuantity,
                        ShelfProductId = customerShelfAction.ShelfProductId,
                        TimeCreated = DateTime.UtcNow
                    };

                    _dbContext.Add(newReplenishmentRequest);

                    await _mediator.Publish(new ReplenishmentRequestCreated()
                    {
                        Barcode = shelfProduct.Product.Barcode,
                        ProductName = shelfProduct.Product.Name,
                        QuantityNeeded = newReplenishmentRequest.QuantityNeeded,
                        CompanyId = companyId
                    });
                }
            }
            else if (customerShelfAction.Type == Domain.Enums.CustomerShelfActionType.Return)
            {
                shelfProduct.CurrentQuantity += customerShelfAction.Quantity;
            }

            _dbContext.Add(customerShelfAction);

            await _dbContext.SaveChangesAsync();

            return Result.Ok(customerShelfAction.Id);
        }
    }
}
