using FluentResults;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface IReplenishmentRequestService
    {
        public Task<Result<IEnumerable<ReplenishmentRequest>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<ReplenishmentRequest, object>> orderBy,
            Expression<Func<ReplenishmentRequest, bool>> predicate = null,
            IEnumerable<Expression<Func<ReplenishmentRequest, object>>> includes = null);

        public Task<Result<ReplenishmentRequest>> GetAsync(Guid companyId, Guid id, IEnumerable<Expression<Func<ReplenishmentRequest, object>>> includes = null);
    }
}
