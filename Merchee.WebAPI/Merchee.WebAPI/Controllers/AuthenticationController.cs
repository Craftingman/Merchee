using Merchee.BLL.Abstractions;
using Merchee.BLL.Models;
using Merchee.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Merchee.WebAPI.Controllers
{
    [Route("auth")]
    public class AuthenticationController : BaseAuthorizedController
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("user/{id?}")]
        public async Task<IActionResult> Get(Guid? id = null)
        {
            if (!id.HasValue)
                id = this.UserId;

            var result = await _authenticationService.GetAsync(this.CompanyId, id.Value);

            return this.HandleResult(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("users")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _authenticationService.FindAllAsync(
                pageNumber,
                pageSize,
                e => e.LastName,
                e => e.CompanyId == this.CompanyId);

            return this.HandleResult(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authenticationService.LoginAsync(model);

            return HandleResult(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserModel model)
        {
            if (model.Role == Domain.Enums.UserRole.SuperAdmin)
                return Forbid();

            var result = await _authenticationService.RegisterUserAsync(this.CompanyId, model);

            return HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("registerUser/{companyId}")]
        public async Task<IActionResult> RegisterUser(Guid companyId, [FromBody] RegisterUserModel model)
        {
            if (model.Role != Domain.Enums.UserRole.Administrator)
                return Forbid();

            var result = await _authenticationService.RegisterUserAsync(companyId, model);

            return HandleResult(result);
        }
    }
}
