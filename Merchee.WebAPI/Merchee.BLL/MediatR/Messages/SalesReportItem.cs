namespace Merchee.BLL.MediatR.Messages
{
    public class SalesReportItem
    {
        public string ProductName { get; set; }
        public string ProductBarcode { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
    }
}
