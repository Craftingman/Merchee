namespace Merchee.Domain.Entities
{
    public class ExpirationWarning : BaseCompanyEntity
    {
        public Guid ShelfItemId { get; set; }
        public ShelfItem ShelfItem { get; set; }

        public DateTime TimeCreated { get; set; }
        public DateTime? TimeCompleted { get; set; }
    }
}
