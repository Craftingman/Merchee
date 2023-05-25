using Merchee.BLL.Abstractions;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Merchee.WebAPI.Controllers
{
    [Route("replenishmentRequests")]
    public class ReplenishmentRequestController : BaseAuthorizedController
    {
        private readonly IReplenishmentRequestService _replenishmentRequestService;

        public ReplenishmentRequestController(IReplenishmentRequestService replenishmentRequestService)
        {
            _replenishmentRequestService = replenishmentRequestService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _replenishmentRequestService.GetAsync(this.CompanyId, id,
                includes: new List<Expression<Func<ReplenishmentRequest, object>>>()
                {
                    s => s.ShelfProduct,
                    s => s.ShelfProduct.Product,
                    s => s.ShelfProduct.Shelf
                });

            return this.HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _replenishmentRequestService.FindAllAsync(
                this.CompanyId,
                pageNumber,
                pageSize,
                e => e.TimeCreated,
                e => e.CompanyId == this.CompanyId,
                includes: new List<Expression<Func<ReplenishmentRequest, object>>>()
                {
                    s => s.ShelfProduct,
                    s => s.ShelfProduct.Product,
                    s => s.ShelfProduct.Shelf
                });

            return this.HandleResult(result);
        }
    }
}
