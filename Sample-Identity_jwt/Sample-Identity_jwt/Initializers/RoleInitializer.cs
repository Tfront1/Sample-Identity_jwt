using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Initializers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = Role.CreateEnumerations().Values;

            await ClearRoles(roles, roleManager);

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role.Name) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role.Name));
                }
            }
        }

        private static async Task ClearRoles(Dictionary<int, Role>.ValueCollection roles,RoleManager<IdentityRole> roleManager)
        {
            var currentDbRoles = roleManager.Roles.ToList();
            foreach (var currentDbRole in currentDbRoles)
            {
                bool roleFinded = false;
                foreach (var role in roles)
                {
                    if (role.Name == currentDbRole.Name){
                        roleFinded = true;
                    }
                }
                if (!roleFinded)
                {
                    await roleManager.DeleteAsync(currentDbRole);
                }
            }
        }
    }
}
