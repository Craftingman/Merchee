namespace Merchee.Domain.Entities
{
    public class UserNotification : BaseCompanyEntity
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }
        public bool Read { get; set; }
    }
}
