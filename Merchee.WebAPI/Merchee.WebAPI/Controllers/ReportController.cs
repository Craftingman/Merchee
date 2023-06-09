using Merchee.BLL.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Merchee.WebAPI.Controllers
{
    [Route("reports")]
    public class ReportController : BaseAuthorizedController
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("salesReport")]
        public async Task<IActionResult> GetSalesReport(DateTime fromDate, DateTime toDate)
        {
            var pdfReport = await _reportService.GetSalesReportPdf(this.CompanyId, fromDate, toDate);
            if (pdfReport == null || !pdfReport.Any())
            {
                return NoContent();
            }

            return File(pdfReport, "application/pdf", $"SalesReport_{fromDate:dd-MM-yyyy}_{toDate:dd-MM-yyyy}.pdf");
        }
    }
}
