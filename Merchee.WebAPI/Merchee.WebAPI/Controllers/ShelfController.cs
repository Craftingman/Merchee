using Merchee.BLL.Abstractions;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Merchee.WebAPI.Controllers
{
    [Route("shelves")]
    public class ShelfController : BaseAuthorizedController
    {
        private readonly IShelfService _shelfService;

        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _shelfService.GetAsync(this.CompanyId, id);

            return this.HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _shelfService.FindAllAsync(
                this.CompanyId,
                pageNumber,
                pageSize,
                e => e.Barcode,
                e => e.CompanyId == this.CompanyId);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Shelf shelf)
        {
            var result = await _shelfService.AddAsync(this.CompanyId, shelf);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Shelf shelf)
        {
            var result = await _shelfService.UpdateAsync(this.CompanyId, id, shelf);

            return this.HandleResult(result);
        }
    }
}
