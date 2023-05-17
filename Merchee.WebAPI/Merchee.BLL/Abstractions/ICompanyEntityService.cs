using FluentResults;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface ICompanyEntityService<TEntity>
        where TEntity : BaseCompanyEntity
    {
        public Task<Result<IEnumerable<TEntity>>> FindAll(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<TEntity, object>> orderBy,
            Expression<Func<TEntity, bool>> predicate);

        public Task<Result<TEntity>> Get(Guid companyId, Guid id);

        public Task<Result<Guid>> Add(TEntity entity);

        public Task<Result> Update(Guid companyId, Guid id, TEntity entity);
      
    }
}
