using Merchee.BLL.Abstractions;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Merchee.WebAPI.Controllers
{
    [Route("notifications")]
    public class NotificationsController : BaseAuthorizedController
    {
        private readonly IUserNotificationService _notificationService;

        public NotificationsController(IUserNotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _notificationService.GetAsync(this.CompanyId, id);

            return this.HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _notificationService.FindAllAsync(
                this.CompanyId,
                pageNumber,
                pageSize,
                e => e.Notification.TimeCreated,
                e => e.UserId == this.UserId,
                includes: new List<Expression<Func<UserNotification, object>>>()
                {
                    s => s.User,
                    s => s.Notification
                });

            return this.HandleResult(result);
        }

        [HttpPut("markAsRead/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(Guid notificationId)
        {
            var result = await _notificationService.MarkAsRead(this.CompanyId, notificationId);

            return this.HandleResult(result);
        }
    }
}
