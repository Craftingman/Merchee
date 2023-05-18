using Merchee.BLL.Abstractions;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Merchee.WebAPI.Controllers
{
    [Route("companies")]
    public class CompanyController : BaseAuthorizedController
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (this.CompanyId != id && this.UserRole != Domain.Enums.UserRole.SuperAdmin)
                return Forbid();

            var result = await _companyService.GetAsync(id);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 0, int pageSize = 10)
        {
            var result = await _companyService.FindAllAsync(pageNumber, pageSize, e => e.Name);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            var result = await _companyService.AddAsync(company);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Company company)
        {
            if (this.CompanyId != id && this.UserRole != Domain.Enums.UserRole.SuperAdmin)
                return Forbid();

            var result = await _companyService.UpdateAsync(id, company);

            return this.HandleResult(result);
        }
    }
}
