using FluentResults;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface IShelfProductService
    {
        public Task<Result<IEnumerable<ShelfProduct>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<ShelfProduct, object>> orderBy,
            Expression<Func<ShelfProduct, bool>> predicate = null,
            IEnumerable<Expression<Func<ShelfProduct, object>>> includes = null);

        public Task<Result<ShelfProduct>> GetAsync(Guid companyId, Guid id, IEnumerable<Expression<Func<ShelfProduct, object>>> includes = null);

        public Task<Result<Guid>> AddAsync(Guid companyId, ShelfProduct entity);

        public Task<Result> UpdateAsync(Guid companyId, Guid id, ShelfProduct entity);
    }
}
