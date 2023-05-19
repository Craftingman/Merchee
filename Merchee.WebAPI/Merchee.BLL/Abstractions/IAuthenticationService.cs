using FluentResults;
using Merchee.BLL.Models;
using Merchee.BusinessLogic.Models;
using Merchee.Domain.Entities;
using System.Linq.Expressions;

namespace Merchee.BLL.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<Result<AuthenticationResult>> LoginAsync(LoginModel model);

        public Task<Result<Guid>> RegisterUserAsync(Guid companyId, RegisterUserModel model);

        public Task<Result<User>> GetAsync(Guid companyId, Guid id);

        public Task<Result<IEnumerable<User>>> FindAllAsync(
            int pageNumber, int pageSize,
            Expression<Func<User, object>> orderBy,
            Expression<Func<User, bool>> predicate = null);
    }
}
