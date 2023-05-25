using AutoMapper;
using Merchee.DataAccess;
using Merchee.Domain.Entities;

namespace Merchee.BLL.Services
{
    public class ExpirationWarningService : BaseCompanyEntityService<ExpirationWarning>
    {
        public ExpirationWarningService(CompanyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
