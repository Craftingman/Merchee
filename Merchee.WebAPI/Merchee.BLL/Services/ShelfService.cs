using AutoMapper;
using FluentResults;
using Merchee.BLL.Abstractions;
using Merchee.BLL.Errors;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Merchee.BLL.Services
{
    public class ShelfService : BaseCompanyEntityService<Shelf>, IShelfService
    {
        public ShelfService(CompanyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Result<IEnumerable<Shelf>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<Shelf, object>> orderBy,
            Expression<Func<Shelf, bool>> predicate = null)
        {
            var query = _dbContext.Set<Shelf>().AsQueryable();


            query = query.Include(s => s.ShelfProducts.Where(sp => sp.Active))
                .ThenInclude(sp => sp.Product);

            query = query.Where(e => e.CompanyId == companyId);

            return await query.OrderByDescending(orderBy)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public async Task<Result<string>> RegisterShelfAsync(Guid companyId, Shelf entity)
        {
            entity.AccessToken = StringHelper.GetRandomlyGenerateBase64String(32);

            var baseResult = await base.AddAsync(companyId, entity);
            var result = baseResult.ToResult((original) => entity.AccessToken);

            return result;
        }
    }
}
