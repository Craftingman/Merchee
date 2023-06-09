using Merchee.BLL.Abstractions;
using Merchee.BLL.MediatR.Messages;
using Merchee.DataAccess;
using Merchee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Merchee.BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly CompanyDbContext _companyDbContext;
        public ReportService(CompanyDbContext companyDbContext) { 
            _companyDbContext = companyDbContext;
        }

        public async Task<byte[]> GetSalesReportPdf(Guid companyId, DateTime fromDate, DateTime toDate)
        {
            var salesReportItems = await _companyDbContext.Set<CustomerShelfAction>()
                .Where(e => e.CompanyId == companyId && e.Time >= fromDate && e.Time <= toDate)
                .Include(e => e.ShelfProduct)
                .ThenInclude(e => e.Product)
                .GroupBy(e => new { e.ShelfProduct.ProductID, e.ShelfProduct.Product.Name, e.ShelfProduct.Product.Barcode })
                .Select(g => new SalesReportItem() { 
                    ProductBarcode = g.Key.Barcode,
                    ProductName = g.Key.Name,
                    Quantity = g.Sum(e => e.Quantity),
                    Total = g.Sum(e => e.Price * e.Quantity)
                })
                .OrderBy(e => e.ProductName)
                .ToListAsync();

            var htmlLayout = this.GenerateSalesReportHtml(fromDate, toDate, salesReportItems);
            var renderer = new ChromePdfRenderer();
            using var pdf = await renderer.RenderHtmlAsPdfAsync(htmlLayout);

            return pdf.BinaryData;
        }

        private string GenerateSalesReportHtml(DateTime fromDate, DateTime toDate, IEnumerable<SalesReportItem> items)
        {
            var total = 0f;
            var sb = new StringBuilder();
            sb.AppendLine($"<h1>Звіт з продажів {fromDate.ToString("dd-MM-yyyy")} - {toDate.ToString("dd-MM-yyyy")}</h1>");
            sb.AppendLine($"<table>");

            sb.AppendLine($"<thead>");
            sb.AppendLine($"<tr>");
            sb.AppendLine($"<td>Продукт</td>");
            sb.AppendLine($"<td>Штрихкод продукту</td>");
            sb.AppendLine($"<td>Загальна кількість</td>");
            sb.AppendLine($"<td>Сума</td>");
            sb.AppendLine($"</tr>");
            sb.AppendLine($"</thead>");

            sb.AppendLine($"<tbody>");
            foreach(var item in items )
            {
                total += item.Total;
                sb.AppendLine(@$"<tr>
                    <td>{item.ProductName}</td>
                    <td>{item.ProductBarcode}</td>
                    <td>{item.Quantity}</td>
                    <td>{item.Total}</td>
                </tr>");
            }
            sb.AppendLine($"</tbody>");

            sb.AppendLine($"<tfoot>");
                sb.AppendLine(@$"<tr>
                    <td></td>
                    <td></td>
                    <td><b>Усього:</b></td>
                    <td><b>{total}</b></td>
                </tr>");
            sb.AppendLine($"</tfoot>");

            sb.AppendLine($"</table>");

            sb.AppendLine(@"<style>
                    table,
                    td {
                        border: 1px solid #333;
                        padding: 2px;
                    }
        
                    thead
                            {
                        background-color: #333;
                        color: #fff;
                    }
                </style>");

            return sb.ToString();
        }
    }
}
