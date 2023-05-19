using AutoMapper;
using FluentResults;
using Merchee.BLL.Abstractions;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Merchee.BLL.Services
{
    public class ShelfProductService : BaseCompanyEntityService<ShelfProduct>, IShelfProductService
    {
        public ShelfProductService(CompanyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<Guid>> AddAsync(Guid companyId, ShelfProduct entity)
        {
            var prevShelfProduct = await _dbContext.Set<ShelfProduct>()
                .FirstOrDefaultAsync(sp => sp.Active && sp.CompanyId == companyId && sp.ShelfID == entity.ShelfID);

            var result = await base.AddAsync(companyId, entity);
            if(result.IsFailed)
                return result;

            if (prevShelfProduct != null)
            {
                prevShelfProduct.Active = false;
                await _dbContext.SaveChangesAsync();
            }

            return result;
        }
    }
}
