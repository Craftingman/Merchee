using Merchee.Domain.Enums;

namespace Merchee.BusinessLogic.Models
{
    public class RegisterUserModel
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
