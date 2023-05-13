namespace Merchee.Domain.Entities
{
    public class Notification : BaseCompanyEntity
    {
        public string Message { get; set; } = string.Empty;
        public DateTime TimeCreated { get; set; }
    }
}
