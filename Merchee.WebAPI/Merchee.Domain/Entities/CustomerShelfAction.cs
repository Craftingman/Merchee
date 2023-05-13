namespace Merchee.Domain.Entities
{
    public class CustomerShelfAction : BaseCompanyEntity
    {
        public Guid ShelfId { get; set; }
        public Shelf Shelf { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
