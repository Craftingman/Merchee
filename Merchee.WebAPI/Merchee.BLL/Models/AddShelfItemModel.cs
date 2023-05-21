namespace Merchee.BLL.Models
{
    public class AddShelfItemModel
    {
        public Guid ShelfProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateManufactured { get; set; }
    }
}
