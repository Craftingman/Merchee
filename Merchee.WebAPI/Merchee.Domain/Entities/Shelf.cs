namespace Merchee.Domain.Entities
{
    public class Shelf : BaseCompanyEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public float MaxWeight { get; set; }
    }
}
