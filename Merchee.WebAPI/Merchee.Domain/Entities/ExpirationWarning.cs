namespace Merchee.Domain.Entities
{
    public class ExpirationWarning : BaseCompanyEntity
    {
        public Guid ShelfProductId { get; set; }
        public ShelfProduct ShelfProduct { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime ProductDateManufactured { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeCompleted { get; set; }
    }
}
