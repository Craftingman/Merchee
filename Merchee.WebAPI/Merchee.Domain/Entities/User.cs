using Merchee.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Merchee.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Guid CompanyId { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
