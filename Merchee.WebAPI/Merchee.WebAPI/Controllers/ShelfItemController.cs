using Merchee.BLL.Abstractions;
using Merchee.BLL.Models;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Merchee.WebAPI.Controllers
{
    [Route("shelfItems")]
    public class ShelfItemController : BaseAuthorizedController
    {
        private readonly IShelfItemService _shelfItemService;

        public ShelfItemController(IShelfItemService shelfItemService)
        {
            _shelfItemService = shelfItemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _shelfItemService.GetAsync(this.CompanyId, id,
                includes: new List<Expression<Func<ShelfItem, object>>>()
                {
                    s => s.ShelfProduct,
                    s => s.ShelfProduct.Product
                });

            return this.HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10, Guid? shelfProductId = null)
        {
            Expression<Func<ShelfItem, bool>> predicate;
            if (shelfProductId.HasValue)
            {
                predicate = s => s.ShelfProductId == shelfProductId;
            }

            var result = await _shelfItemService.FindAllAsync(
                this.CompanyId,
                pageNumber,
                pageSize,
                e => e.DateAdded,
                e => e.CompanyId == this.CompanyId,
                includes: new List<Expression<Func<ShelfItem, object>>>()
                {
                    s => s.ShelfProduct,
                    s => s.ShelfProduct.Product
                });

            return this.HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddShelfItemModel model)
        {
            var result = await _shelfItemService.AddAsync(this.CompanyId, this.UserId, model);

            return this.HandleResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Post([FromBody] RemoveShelfItemModel model)
        {
            var result = await _shelfItemService.RemoveAsync(this.CompanyId, this.UserId, model);

            return this.HandleResult(result);
        }
    }
}
