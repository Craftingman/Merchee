using Merchee.BLL.Abstractions;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Merchee.WebAPI.Controllers
{
    [Route("shelfProducts")]
    public class ShelfProductController : BaseAuthorizedController
    {
        private readonly IShelfProductService _shelfProductService;

        public ShelfProductController(IShelfProductService shelfProductService)
        {
            _shelfProductService = shelfProductService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _shelfProductService.GetAsync(this.CompanyId, id,
                includes: new List<Expression<Func<ShelfProduct, object>>>()
                {
                    s => s.Shelf,
                    s => s.Product
                });

            return this.HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _shelfProductService.FindAllAsync(
                this.CompanyId,
                pageNumber,
                pageSize,
                e => e.Location,
                e => e.CompanyId == this.CompanyId,
                includes: new List<Expression<Func<ShelfProduct, object>>>()
                {
                    s => s.Shelf,
                    s => s.Product
                });

            return this.HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShelfProduct shelf)
        {
            var result = await _shelfProductService.AddAsync(this.CompanyId, shelf);

            return this.HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ShelfProduct shelf)
        {
            var result = await _shelfProductService.UpdateAsync(this.CompanyId, id, shelf);

            return this.HandleResult(result);
        }
    }
}
