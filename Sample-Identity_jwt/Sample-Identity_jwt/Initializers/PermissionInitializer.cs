using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Contextes;
using Sample_Identity_jwt.Models;
using Sample_Identity_jwt.PermissionModule;
using System.Collections.Generic;

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

            await ClearPermissions(permissions, context);

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

        private static async Task ClearPermissions(IEnumerable<Permission> permissions, ApplicationContext context)
        {
            var currentDbPermissions = context.Permissions.ToList();
            foreach (var currentDbPermission in currentDbPermissions)
            {
                bool permissionFinded = false;
                foreach (var permission in permissions)
                {
                    if (permission.Name == currentDbPermission.Name)
                    {
                        permissionFinded = true;
                    }
                }
                if (!permissionFinded)
                {
                    context.Permissions.Remove(currentDbPermission);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
