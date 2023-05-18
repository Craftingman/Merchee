using Merchee.Domain.Enums;

namespace Merchee.Domain.Entities
{
    public class CustomerShelfAction : BaseCompanyEntity
    {
        public Guid ShelfProductId { get; set; }
        public ShelfProduct ShelfProduct { get; set; }

        public CustomerShelfActionType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
