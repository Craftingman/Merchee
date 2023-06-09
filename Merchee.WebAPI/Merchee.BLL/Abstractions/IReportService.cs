namespace Merchee.BLL.Abstractions
{
    public interface IReportService
    {
        Task<byte[]> GetSalesReportPdf(Guid companyId, DateTime fromDate, DateTime toDate);
    }
}
