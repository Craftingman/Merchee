namespace Merchee.Domain.Entities
{
    public class Shelf : BaseCompanyEntity
    {
        public string Barcode { get; set; }
        public float MaxWeight { get; set; }
        public string AccessToken { get; set; }

        public ICollection<ShelfProduct> ShelfProducts { get; set; }
    }
}
