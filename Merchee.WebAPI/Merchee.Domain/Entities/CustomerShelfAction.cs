namespace Merchee.Domain.Entities
{
    public class CustomerShelfAction : BaseCompanyEntity
    {
        public Guid ShelfId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
