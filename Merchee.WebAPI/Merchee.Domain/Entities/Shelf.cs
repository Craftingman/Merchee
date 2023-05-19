namespace Merchee.Domain.Entities
{
    public class Shelf : BaseCompanyEntity
    {
        public string Barcode { get; set; }
        public float MaxWeight { get; set; }
    }
}
