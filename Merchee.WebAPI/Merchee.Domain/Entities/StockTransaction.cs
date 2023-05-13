using Merchee.Domain.Enums;

namespace Merchee.Domain.Entities
{
    public class StockTransaction : BaseCompanyEntity
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public Guid ShelfID { get; set; }
        public Shelf Shelf { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public StockTransactionType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
