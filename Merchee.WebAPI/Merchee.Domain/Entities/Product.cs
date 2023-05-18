namespace Merchee.Domain.Entities
{
    public class Product : BaseCompanyEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public float Price { get; set; }
        public float FullWeight { get; set; }
        public int ShelfLifeTimeDays { get; set; }
    }
}
