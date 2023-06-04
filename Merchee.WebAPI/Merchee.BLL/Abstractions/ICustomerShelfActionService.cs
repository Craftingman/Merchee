using FluentResults;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface ICustomerShelfActionService
    {
        public Task<Result<IEnumerable<CustomerShelfAction>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<CustomerShelfAction, object>> orderBy,
            Expression<Func<CustomerShelfAction, bool>> predicate = null,
            IEnumerable<Expression<Func<CustomerShelfAction, object>>> includes = null);

        public Task<Result<CustomerShelfAction>> GetAsync(Guid companyId, Guid id, IEnumerable<Expression<Func<CustomerShelfAction, object>>> includes = null);

        public Task<Result<Guid>> AddAsync(CustomerShelfAction customerShelfAction, string accessToken);
    }
}
