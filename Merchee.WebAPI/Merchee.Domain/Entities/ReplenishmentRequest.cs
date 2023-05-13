namespace Merchee.Domain.Entities
{
    public class ReplenishmentRequest : BaseCompanyEntity
    {
        public Guid ProductID { get; set; }
        public Guid ShelfID { get; set; }
        public int QuantityNeeded { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeCompleted { get; set; }
    }
}
