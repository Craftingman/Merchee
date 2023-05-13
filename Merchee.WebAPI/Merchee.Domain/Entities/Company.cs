namespace Merchee.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string CompanyEmail { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
    }
}
