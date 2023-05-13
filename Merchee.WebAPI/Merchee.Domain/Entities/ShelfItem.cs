namespace Merchee.Domain.Entities
{
    public class ShelfItem : BaseCompanyEntity
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public Guid ShelfID { get; set; }
        public Shelf Shelf { get; set; }

        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
