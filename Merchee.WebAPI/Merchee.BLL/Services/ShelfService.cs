using AutoMapper;
using FluentResults;
using Merchee.BLL.Abstractions;
using Merchee.DataAccess;
using Merchee.Domain.Entities;

namespace Merchee.BLL.Services
{
    public class ShelfService : BaseCompanyEntityService<Shelf>, IShelfService
    {
        public ShelfService(CompanyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
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
