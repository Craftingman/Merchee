using FluentResults;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface ICompanyService
    {
        public Task<Result<IEnumerable<Company>>> FindAllAsync(
            int pageNumber, int pageSize,
            Expression<Func<Company, object>> orderBy,
            Expression<Func<Company, bool>> predicate = null);

        public Task<Result<Company>> GetAsync(Guid id);

        public Task<Result<Guid>> AddAsync(Company entity);

        public Task<Result> UpdateAsync(Guid id, Company entity);
    }
}
