using Merchee.Domain.Enums;

namespace Merchee.Domain.Entities
{
    public class StockTransaction : BaseCompanyEntity
    {
        public Guid ProductId { get; set; }
        public Guid ShelfId { get; set; }
        public Guid UserId { get; set; }
        public StockTransactionType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
