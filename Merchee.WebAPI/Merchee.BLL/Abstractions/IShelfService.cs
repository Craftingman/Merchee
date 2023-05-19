using FluentResults;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface IShelfService
    {
        public Task<Result<IEnumerable<Shelf>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<Shelf, object>> orderBy,
            Expression<Func<Shelf, bool>> predicate = null,
            IEnumerable<Expression<Func<Shelf, object>>> includes = null);

        public Task<Result<Shelf>> GetAsync(Guid companyId, Guid id, IEnumerable<Expression<Func<Shelf, object>>> includes = null);

        public Task<Result<Guid>> AddAsync(Guid companyId, Shelf entity);

        public Task<Result> UpdateAsync(Guid companyId, Guid id, Shelf entity);
    }
}
