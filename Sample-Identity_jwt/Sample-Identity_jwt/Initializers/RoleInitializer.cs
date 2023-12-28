using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Initializers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = Role.CreateEnumerations().Values;
            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role.Name) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role.Name));
                }
            }
        }
    }
}
