namespace Merchee.Domain.Entities
{
    public class ShelfItem : BaseCompanyEntity
    {
        public Guid ShelfId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
