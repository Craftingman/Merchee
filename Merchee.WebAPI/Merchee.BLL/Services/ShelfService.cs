using AutoMapper;
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
    }
}
