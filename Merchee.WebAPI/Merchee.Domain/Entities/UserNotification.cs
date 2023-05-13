namespace Merchee.Domain.Entities
{
    public class UserNotification : BaseCompanyEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }

        public bool Read { get; set; }
    }
}
