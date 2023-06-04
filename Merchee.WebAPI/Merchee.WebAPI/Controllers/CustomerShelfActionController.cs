using Merchee.BLL.Abstractions;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Merchee.WebAPI.Controllers
{
    [Route("customerShelfActions")]
    public class CustomerShelfActionController : BaseAuthorizedController
    {
        private readonly ICustomerShelfActionService _customerShelfActionService;

        public CustomerShelfActionController(ICustomerShelfActionService customerShelfActionService)
        {
            _customerShelfActionService = customerShelfActionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _customerShelfActionService.GetAsync(this.CompanyId, id);

            return this.HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10, Guid? shelfProductId = null)
        {
            Expression<Func<CustomerShelfAction, bool>> predicate;
            if (shelfProductId.HasValue)
            {
                predicate = s => s.ShelfProductId == shelfProductId && s.CompanyId == this.CompanyId;
            }
            else
            {
                predicate = s => s.CompanyId == this.CompanyId;
            }

            var result = await _customerShelfActionService.FindAllAsync(
                this.CompanyId,
                pageNumber,
                pageSize,
                e => e.Time,
                predicate);

            return this.HandleResult(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerShelfAction product)
        {
            Request.Headers.TryGetValue("Access-Token", out var acccessToken);

            var result = await _customerShelfActionService.AddAsync(product, acccessToken);

            return this.HandleResult(result);
        }
    }
}
