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
