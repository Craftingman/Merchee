namespace Merchee.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string CompanyEmail { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
    }
}
