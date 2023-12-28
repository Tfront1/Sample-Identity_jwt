using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sample_Identity_jwt.Contextes;
using Sample_Identity_jwt.Migrations;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationContext context;

        public PermissionService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
        {
            var roleId = await context.UserRoles
                .Where(ur => ur.UserId == userId.ToString())
                .Select(ur => ur.RoleId)
                .FirstOrDefaultAsync();

            var roleName = await context.Roles
                .Where(ur => ur.Id == roleId)
                .Select(ur => ur.Name)
                .FirstOrDefaultAsync();

            return context.RolePermissions
                .Where(rp => rp.RoleName == roleName)
                .Select(rp => rp.PermissionName)
                .ToHashSet();
        }
    }
}
