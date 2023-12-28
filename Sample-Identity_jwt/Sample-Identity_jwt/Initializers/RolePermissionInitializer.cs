using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Contextes;
using Sample_Identity_jwt.Models;
using Sample_Identity_jwt.PermissionModule;

namespace Sample_Identity_jwt.Initializers
{
    public class RolePermissionInitializer
    {
        public static async Task InitializeAsync(ApplicationContext context)
        {
            await AddRolePermission(context, Role.Admin, Permissions.GetWeather);
        }

        private static async Task AddRolePermission(ApplicationContext context, Role role, Permissions permission)
        {
            if (!context.RolePermissions
                    .Any(p => p.RoleName == role.Name && p.PermissionName == permission.ToString()))
            {
                await context.RolePermissions.AddAsync(Create(role, permission));
                await context.SaveChangesAsync();
            }
        }

        private static RolePermission Create(
                Role role,
                Permissions permission)
        {
            return new RolePermission
            {
                RoleName = role.Name,
                PermissionName = permission.ToString()
            };
        }
    }
}
