using Merchee.BLL.Abstractions;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Merchee.WebAPI.Controllers
{
    [Route("products")]
    public class ProductController : BaseAuthorizedController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _productService.GetAsync(this.CompanyId, id);

            return this.HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _productService.FindAllAsync(
                this.CompanyId,
                pageNumber,
                pageSize,
                e => e.Name,
                e => e.CompanyId == this.CompanyId);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "Merchandiser, Employee")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var result = await _productService.AddAsync(this.CompanyId, product);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "Merchandiser, Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Product product)
        {
            var result = await _productService.UpdateAsync(this.CompanyId, id, product);

            return this.HandleResult(result);
        }
    }
}
