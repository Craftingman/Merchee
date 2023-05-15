using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Merchee.DataAccess
{
    public static class CompanyDbInitializer
    {
        static readonly Guid MercheeCompanyId = Guid.Parse("10000000-2000-3003-0002-000000000001");

        public static void SeedCompany(CompanyDbContext context)
        {
            var mercheeCompany = context.Set<Company>().Find(MercheeCompanyId);
            if (mercheeCompany is not null)
            {
                return;
            }

            mercheeCompany = new Company
            {
                Id = MercheeCompanyId,
                Name = "Merchee Company",
                CompanyEmail = "main@merchee.com",
                Currency = "UAH"
            };

            context.Set<Company>().Add(mercheeCompany);
            context.SaveChanges();
        }

        public static void SeedSuperAdmin(
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (userManager.FindByEmailAsync("viktor.zherebnyi@nure.ua").Result == null)
            {
                User user = new User
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    CompanyId = MercheeCompanyId,
                    UserName = "viktor.zherebnyi@nure.ua",
                    Email = "viktor.zherebnyi@nure.ua"
                };

                if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
                {
                    roleManager.CreateAsync(new IdentityRole<Guid>("SuperAdmin")).Wait();
                }

                IdentityResult result = userManager.CreateAsync(user, "superadmin1337").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
            }
        }
    }
}