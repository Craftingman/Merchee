using MediatR;

namespace Merchee.BLL.MediatR.Messages
{
    public class ReplenishmentRequestCreated : BaseCompanyMessage, INotification
    {
        public int QuantityNeeded { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
    }
}
