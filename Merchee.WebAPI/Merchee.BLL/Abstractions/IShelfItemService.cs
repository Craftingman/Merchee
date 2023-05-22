using FluentResults;
using Merchee.BLL.Models;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface IShelfItemService
    {
        public Task<Result<IEnumerable<ShelfItem>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<ShelfItem, object>> orderBy,
            Expression<Func<ShelfItem, bool>> predicate = null,
            IEnumerable<Expression<Func<ShelfItem, object>>> includes = null);

        public Task<Result<ShelfItem>> GetAsync(Guid companyId, Guid id,
            IEnumerable<Expression<Func<ShelfItem, object>>> includes = null);

        public Task<Result<Guid>> AddAsync(Guid companyId, Guid userId, AddShelfItemModel model);

        public Task<Result> RemoveAsync(Guid companyId, Guid userId, RemoveShelfItemModel model);
    }
}
