using FluentResults;
using Merchee.BLL.Models;
using Merchee.BusinessLogic.Models;

namespace Merchee.BLL.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<Result<AuthenticationResult>> LoginAsync(LoginModel model);

        public Task<Result> RegisterUserAsync(Guid companyId, RegisterUserModel model);
    }
}
