using AutoMapper;
using Merchee.BLL.Abstractions;
using Merchee.DataAccess;
using Merchee.Domain.Entities;

namespace Merchee.BLL.Services
{
    public class ProductService : BaseCompanyEntityService<Product>, IProductService
    {
        public ProductService(CompanyDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
