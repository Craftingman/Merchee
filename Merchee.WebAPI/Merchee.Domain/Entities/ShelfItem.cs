namespace Merchee.Domain.Entities
{
    // This gets deactivated once ExpirationWarning is completed for it
    public class ShelfItem : BaseCompanyEntity
    {
        public Guid ShelfProductId { get; set; }
        public ShelfProduct ShelfProduct { get; set; }
        
        public int Quantity { get; set; }
        public DateTime ManufacturedAt { get; set; }
    }
}
