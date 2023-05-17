using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Merchee.WebAPI.Controllers
{
    [Authorize]
    public class BaseAuthorizedController : Controller
    {
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
