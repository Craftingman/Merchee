using FluentResults;
using Merchee.BLL.Abstractions;
using Merchee.BLL.Errors;
using Merchee.BLL.Models;
using Merchee.BusinessLogic.Models;
using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Merchee.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IOptions<JwtSettings> options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = options.Value;
        }

        public async Task<Result<AuthenticationResult>> LoginAsync(LoginModel model)
        {
            if (model is null
                || string.IsNullOrWhiteSpace(model.Email)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                return Result.Fail(new BadRequestError());
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return Result.Fail<AuthenticationResult>(new NotFoundError());
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Result.Fail<AuthenticationResult>(new UnauthorizedError());
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = this.GenerateJwtToken(user.Id, user.CompanyId, user.FirstName, user.LastName, userRoles);

            return Result.Ok(new Merchee.BLL.Models.AuthenticationResult()
            {
                Token = token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        public async Task<Result> RegisterUserAsync(Guid companyId, RegisterUserModel model)
        {
            if (model is null
                || string.IsNullOrWhiteSpace(model.Email)
                || string.IsNullOrWhiteSpace(model.FirstName)
                || string.IsNullOrWhiteSpace(model.LastName)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                return Result.Fail(new BadRequestError());
            }

            if (model.Password != model.RepeatPassword)
            {
                return Result.Fail("Passwords don't match");
            }

            var userExists = (await _userManager.FindByEmailAsync(model.Email)) is not null;
            if (userExists)
            {
                return Result.Fail("Duplicate email");
            }

            var user = new User();

            user.Id = Guid.NewGuid();
            user.CompanyId = companyId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;

            if (!await _roleManager.RoleExistsAsync(model.Role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(model.Role.ToString()));
            }

            var userResult = await _userManager.CreateAsync(user, model.Password);
            if (!userResult.Succeeded)
            {
                return Result.Fail(userResult.Errors
                    .Select(e => new Error($"{e.Code} - {e.Description}")));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, model.Role.ToString());
            if (!roleResult.Succeeded)
            {
                return Result.Fail(userResult.Errors
                    .Select(e => new Error($"{e.Code} - {e.Description}")));
            }

            return Result.Ok();
        }

        private string GenerateJwtToken(
            Guid userId,
            Guid companyId,
        string firstName,
        string lastName,
            IEnumerable<string> roles)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim("company_id", companyId.ToString())
            };

            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
