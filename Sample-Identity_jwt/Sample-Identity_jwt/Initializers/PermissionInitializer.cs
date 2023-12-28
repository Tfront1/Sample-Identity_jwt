using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Contextes;
using Sample_Identity_jwt.Models;
using Sample_Identity_jwt.PermissionModule;

namespace Sample_Identity_jwt.Initializers
{
    public class PermissionInitializer
    {
        public static async Task InitializeAsync(ApplicationContext context)
        {
            IEnumerable<Permission> permissions = Enum.GetValues<Permissions>()
                .Select(p => new Permission
                {
                    Name = p.ToString(),
                });
            foreach (var permission in permissions)
            {
                if (!context.Permissions
                    .Any(p => p.Name == permission.Name))
                {
                    await context.Permissions.AddAsync(permission);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
