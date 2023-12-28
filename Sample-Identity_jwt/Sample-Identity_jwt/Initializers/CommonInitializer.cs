using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Contextes;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Initializers
{
    public class CommonInitializer
    {
        public static async Task InitializeAppAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer.InitializeAsync(roleManager);
            }

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                await PermissionInitializer.InitializeAsync(context);
                await RolePermissionInitializer.InitializeAsync(context);
            }
        }
    }
}
