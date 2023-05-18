namespace Merchee.Domain.Entities
{
    public class ShelfProduct : BaseCompanyEntity
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public Guid ShelfID { get; set; }
        public Shelf Shelf { get; set; }

        public ICollection<ShelfItem> Items { get; set; }

        public int CurrentQuantity { get; set; }
        // CurrentQuantity <= MinQuantity - Create ReplanishmentRequest
        public int MinQuantity { get; set; }
    }
}
