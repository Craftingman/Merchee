using FluentResults;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface IUserNotificationService
    {
        public Task<Result<IEnumerable<UserNotification>>> FindAllAsync(
            Guid companyId,
            int pageNumber, int pageSize,
            Expression<Func<UserNotification, object>> orderBy,
            Expression<Func<UserNotification, bool>> predicate = null,
            IEnumerable<Expression<Func<UserNotification, object>>> includes = null);

        public Task<Result<UserNotification>> GetAsync(Guid companyId, Guid id, IEnumerable<Expression<Func<UserNotification, object>>> includes = null);

        public Task<Result> MarkAsRead(Guid companyId, Guid userNotificationId);
    }
}
