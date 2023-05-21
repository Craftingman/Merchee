namespace Merchee.Domain.Entities
{
    // This gets deactivated once ExpirationWarning is completed for it
    public class ShelfItem : BaseCompanyEntity
    {
        public Guid ShelfProductId { get; set; }
        public ShelfProduct ShelfProduct { get; set; }

        public Guid AddedByUserId { get; set; }
        public User AddedByUser { get; set; }

        public Guid? RemovedByUserId { get; set; }
        public User? RemovedByUser { get; set; }

        public DateTime DateManufactured { get; set; }
        public int QuantityAdded { get; set; }
        public int? QuantityRemoved { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateRemoved { get; set; }
    }
}
