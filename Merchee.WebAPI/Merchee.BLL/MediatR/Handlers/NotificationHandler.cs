using MediatR;
using Merchee.BLL.MediatR.Messages;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Merchee.BLL.MediatR.Handlers
{
    public class NotificationHandler : INotificationHandler<ReplenishmentRequestUpdated>,
         INotificationHandler<ReplenishmentRequestCreated>,
         INotificationHandler<ReplenishmentRequestCompleted>
    {
        protected CompanyDbContext _dbContext;

        public NotificationHandler(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ReplenishmentRequestUpdated notification, CancellationToken cancellationToken)
        {
            var notificationId = Guid.NewGuid();

            _dbContext.Set<Notification>().Add(new Notification()
            {
                Active = true,
                Id = notificationId,
                CompanyId = notification.CompanyId,
                TimeCreated = DateTime.UtcNow,
                Message = $"Replenishment request for shelf " +
                    $"{notification.Barcode} : {notification.ProductName} was update with new " +
                    $"quantity needed ({notification.NewQuantityNeeded})"
            });

            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                await this.NotifyUsers(notification.CompanyId, notificationId);
            }
        }

        public async Task Handle(ReplenishmentRequestCreated notification, CancellationToken cancellationToken)
        {
            var notificationId = Guid.NewGuid();

            _dbContext.Set<Notification>().Add(new Notification()
            {
                Active = true,
                Id = notificationId,
                CompanyId = notification.CompanyId,
                TimeCreated = DateTime.UtcNow,
                Message = $"Replenishment request for shelf " + 
                    $"{notification.Barcode} : {notification.ProductName} was created with " +
                    $"quantity needed ({notification.QuantityNeeded})"
            });

            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                await this.NotifyUsers(notification.CompanyId, notificationId);
            }
        }

        public async Task Handle(ReplenishmentRequestCompleted notification, CancellationToken cancellationToken)
        {
            var notificationId = Guid.NewGuid();

            _dbContext.Set<Notification>().Add(new Notification()
            {
                Active = true,
                Id = notificationId,
                CompanyId = notification.CompanyId,
                TimeCreated = DateTime.UtcNow,
                Message = $"Replenishment request for shelf " +
                    $"{notification.Barcode} : {notification.ProductName} was completed"
            });

            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                await this.NotifyUsers(notification.CompanyId, notificationId);
            }
        }

        private async Task NotifyUsers(Guid companyId, Guid notificationId)
        {
            var userIds = await _dbContext.Users.Where(u => u.CompanyId == companyId
                && (u.Role == Domain.Enums.UserRole.Merchandiser || u.Role == Domain.Enums.UserRole.Employee))
                .Select(u => u.Id).ToListAsync();

            var userNotifications = new List<UserNotification>();
            foreach (var userId in userIds)
            {
                userNotifications.Add(new UserNotification()
                {
                    Active = true,
                    Id = Guid.NewGuid(),
                    CompanyId = companyId,
                    NotificationId = notificationId,
                    UserId = userId,
                    Read = false
                });
            }

            _dbContext.AddRange(userNotifications);

            await _dbContext.SaveChangesAsync();
        }
    }
}
