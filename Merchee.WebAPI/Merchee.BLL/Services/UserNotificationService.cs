using AutoMapper;
using FluentResults;
using Merchee.BLL.Abstractions;
using Merchee.BLL.Errors;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Merchee.BLL.Services
{
    public class UserNotificationService : BaseCompanyEntityService<UserNotification>, IUserNotificationService
    {
        public UserNotificationService(CompanyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Result> MarkAsRead(Guid companyId, Guid userNotificationId)
        {
            var originalEntity = await _dbContext.Set<UserNotification>()
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == userNotificationId);
            if (originalEntity == null)
                return Result.Fail(new NotFoundError());

            originalEntity.Read = true;

            await _dbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
