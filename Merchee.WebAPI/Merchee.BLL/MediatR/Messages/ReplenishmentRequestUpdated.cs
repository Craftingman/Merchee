using MediatR;

namespace Merchee.BLL.MediatR.Messages
{
    public class ReplenishmentRequestUpdated : BaseCompanyMessage, INotification
    {
        public int NewQuantityNeeded { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
    }
}
