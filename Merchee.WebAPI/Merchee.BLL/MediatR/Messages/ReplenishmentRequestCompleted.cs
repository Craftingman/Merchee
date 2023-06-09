using MediatR;

namespace Merchee.BLL.MediatR.Messages
{
    public class ReplenishmentRequestCompleted : BaseCompanyMessage, INotification
    {
        public string Barcode { get; set; }
        public string ProductName { get; set; }
    }
}
