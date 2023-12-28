using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Contextes;
using Sample_Identity_jwt.Models;
using Sample_Identity_jwt.PermissionModule;
using System.Collections.Generic;

namespace Sample_Identity_jwt.Initializers
{
    public class RolePermissionInitializer
    {
        private static List<RolePermission> rolePermissions = new List<RolePermission>()
        {
            Create(Role.Admin, Permissions.GetWeather),
            Create(Role.Admin, Permissions.Administration),
            Create(Role.User, Permissions.GetWeather),
        };

        public static async Task InitializeAsync(ApplicationContext context)
        {
            await ClearRolePermissions(rolePermissions, context);

            foreach (var rolePermission in rolePermissions)
            {
                await AddRolePermission(context, rolePermission);
            }
        }

        private static async Task AddRolePermission(ApplicationContext context, RolePermission rolePermission)
        {
            if (!context.RolePermissions
                    .Any(p => p.RoleName == rolePermission.RoleName && p.PermissionName == rolePermission.PermissionName))
            {
                await context.RolePermissions.AddAsync(rolePermission);
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

        private static async Task ClearRolePermissions(List<RolePermission> rolePermissions, ApplicationContext context)
        {
            var currentDbRolePermissions = context.RolePermissions.ToList();
            foreach (var currentDbRolePermission in currentDbRolePermissions)
            {
                bool rolePermissionFinded = false;
                foreach (var rolePermission in rolePermissions)
                {
                    if (rolePermission.RoleName == currentDbRolePermission.RoleName &&
                        rolePermission.PermissionName == currentDbRolePermission.PermissionName)
                    {
                        rolePermissionFinded = true;
                    }
                }
                if (!rolePermissionFinded)
                {
                    context.RolePermissions.Remove(currentDbRolePermission);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
