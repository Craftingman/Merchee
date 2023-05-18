using Merchee.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Merchee.WebAPI.Controllers
{
    [Authorize]
    public class BaseAuthorizedController : BaseController
    {
        protected UserRole UserRole
        {
            get
            {
                var roleName = this.HttpContext.User.Claims
                    .First(c => c.Type == ClaimTypes.Role).Value;

                Enum.TryParse(roleName, out UserRole userRole);

                return userRole;
            }
        }

        protected Guid UserId
        {
            get
            {
                return Guid.Parse(this.HttpContext.User.Claims
                    .First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
        }

        protected Guid CompanyId
        {
            get
            {
                return Guid.Parse(this.HttpContext.User.Claims
                    .First(c => c.Type == "company_id").Value);
            }
        }
    }
}
