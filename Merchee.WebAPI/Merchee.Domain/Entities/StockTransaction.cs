using Merchee.Domain.Enums;

namespace Merchee.Domain.Entities
{
    public class StockTransaction : BaseCompanyEntity
    {
        public Guid ShelfProductId { get; set; }
        public ShelfProduct ShelfProduct { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public StockTransactionType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
