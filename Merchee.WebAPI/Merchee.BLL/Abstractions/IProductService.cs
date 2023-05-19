using FluentResults;
using Merchee.BLL.Errors;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface IProductService
    {
        public Task<Result<IEnumerable<Product>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<Product, object>> orderBy,
            Expression<Func<Product, bool>> predicate = null);

        public Task<Result<Product>> GetAsync(Guid companyId, Guid id);

        public Task<Result<Guid>> AddAsync(Guid companyId, Product entity);

        public Task<Result> UpdateAsync(Guid companyId, Guid id, Product entity);
    }
}
