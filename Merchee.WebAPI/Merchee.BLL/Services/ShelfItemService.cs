using AutoMapper;
using FluentResults;
using MediatR;
using Merchee.BLL.Abstractions;
using Merchee.BLL.Errors;
using Merchee.BLL.MediatR.Messages;
using Merchee.BLL.Models;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Merchee.BLL.Services
{
    public class ShelfItemService : BaseCompanyEntityService<ShelfItem>, IShelfItemService
    {
        private readonly IMediator _mediator;

        public ShelfItemService(CompanyDbContext dbContext, IMapper mapper, IMediator mediator) : base(dbContext, mapper)
        {
            _mediator = mediator;
        }

        public async Task<Result<Guid>> AddAsync(Guid companyId, Guid userId, AddShelfItemModel model)
        {
            var shelfProduct = await _dbContext.Set<ShelfProduct>()
                .Include(sp => sp.Product)
                .FirstOrDefaultAsync(sp => sp.Id == model.ShelfProductId);
            if (shelfProduct is null)
                return Result.Fail(new NotFoundError());

            if (!shelfProduct.Active)
                return Result.Fail(new InactiveItemError());

            var replanishmentRequest = await _dbContext.Set<ReplenishmentRequest>()
                .FirstOrDefaultAsync(e => e.ShelfProductId == model.ShelfProductId && !e.IsCompleted);
            if (replanishmentRequest is not null)
            {
                if (model.Quantity >= replanishmentRequest.QuantityNeeded)
                {
                    replanishmentRequest.IsCompleted = true;
                    replanishmentRequest.TimeCompleted = DateTime.UtcNow;

                    await _mediator.Publish(new ReplenishmentRequestCompleted()
                    {
                        Barcode = shelfProduct.Product.Barcode,
                        ProductName = shelfProduct.Product.Name,
                        CompanyId = companyId
                    });
                }
                else
                {
                    replanishmentRequest.QuantityNeeded -= model.Quantity;

                    await _mediator.Publish(new ReplenishmentRequestUpdated()
                    {
                        Barcode = shelfProduct.Product.Barcode,
                        ProductName = shelfProduct.Product.Name,
                        NewQuantityNeeded = replanishmentRequest.QuantityNeeded,
                        CompanyId = companyId
                    });
                }
            }

            var shelfItem = new ShelfItem()
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId,
                Active = true,
                DateAdded = DateTime.UtcNow.Date,
                AddedByUserId = userId,
                ShelfProductId = model.ShelfProductId,
                QuantityAdded = model.Quantity,
                DateManufactured = model.DateManufactured
            };

            _dbContext.Add(shelfItem);

            shelfProduct.CurrentQuantity += model.Quantity;

            await _dbContext.SaveChangesAsync();

            return Result.Ok(shelfItem.Id);
        }

        public async Task<Result> RemoveAsync(Guid companyId, Guid userId, RemoveShelfItemModel model)
        {
            var shelfItem = await _dbContext.Set<ShelfItem>().Include(e => e.ShelfProduct)
                .ThenInclude(e => e.Items.Where(e => e.Active == true))
                .Include(e => e.ShelfProduct.Product)
                .FirstOrDefaultAsync(e => e.Id == model.ShelfItemId);
            if (shelfItem is null)
                return Result.Fail(new NotFoundError());

            shelfItem.RemovedByUserId = userId;
            shelfItem.QuantityRemoved = model.Quantity;
            shelfItem.Active = false;

            if (!shelfItem.ShelfProduct.Items.Any(e => e.DateManufactured.Date == shelfItem.DateManufactured.Date
                && e.Id != shelfItem.Id && !e.Active))
            {
                var expirationWarning = await _dbContext.Set<ExpirationWarning>()
                    .FirstOrDefaultAsync(e => e.ShelfProductId == shelfItem.ShelfProductId
                        && e.ProductDateManufactured.Date == shelfItem.DateManufactured.Date);
                if (expirationWarning is not null)
                {
                    expirationWarning.IsCompleted = true;
                    expirationWarning.TimeCompleted = DateTime.UtcNow;
                }
            }

            var newShelfProductQuantity = shelfItem.ShelfProduct.CurrentQuantity - model.Quantity;
            shelfItem.ShelfProduct.CurrentQuantity = newShelfProductQuantity >= 0 ? newShelfProductQuantity : 0;

            var replanishmentRequest = await _dbContext.Set<ReplenishmentRequest>()
                .FirstOrDefaultAsync(e => e.ShelfProductId == shelfItem.ShelfProductId && !e.IsCompleted);
            if (replanishmentRequest is not null)
            {
                if (shelfItem.ShelfProduct.CurrentQuantity > 0)
                {
                    replanishmentRequest.QuantityNeeded += model.Quantity;

                    await _mediator.Publish(new ReplenishmentRequestUpdated()
                    {
                        Barcode = shelfItem.ShelfProduct.Product.Barcode,
                        ProductName = shelfItem.ShelfProduct.Product.Name,
                        NewQuantityNeeded = replanishmentRequest.QuantityNeeded,
                        CompanyId = companyId
                    });
                }
            }
            else if (shelfItem.ShelfProduct.CurrentQuantity < shelfItem.ShelfProduct.MinQuantity)
            {
                var newReplenishmentRequest = new ReplenishmentRequest()
                {
                    Active = true,
                    CompanyId = companyId,
                    IsCompleted = false,
                    QuantityNeeded = shelfItem.ShelfProduct.FullCapacity - shelfItem.ShelfProduct.CurrentQuantity,
                    TimeCreated = DateTime.UtcNow,
                    ShelfProductId = shelfItem.ShelfProductId
                };

                _dbContext.Add(newReplenishmentRequest);

                await _mediator.Publish(new ReplenishmentRequestCreated()
                {
                    Barcode = shelfItem.ShelfProduct.Product.Barcode,
                    ProductName = shelfItem.ShelfProduct.Product.Name,
                    QuantityNeeded = newReplenishmentRequest.QuantityNeeded,
                    CompanyId = companyId
                });
            }

            await _dbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
