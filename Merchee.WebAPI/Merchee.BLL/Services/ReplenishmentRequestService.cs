using AutoMapper;
using Merchee.BLL.Abstractions;
using Merchee.DataAccess;
using Merchee.Domain.Entities;

namespace Merchee.BLL.Services
{
    public class ReplenishmentRequestService : BaseCompanyEntityService<ReplenishmentRequest>, IReplenishmentRequestService
    {
        public ReplenishmentRequestService(CompanyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
