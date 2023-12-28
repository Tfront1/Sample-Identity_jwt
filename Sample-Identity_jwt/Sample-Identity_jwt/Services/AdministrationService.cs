using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Contextes;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly ApplicationContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationService(ApplicationContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<List<IdentityUser>> GetUsers()
        {
            return userManager.Users.ToList();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<List<Permission>> GetPermissions()
        {
            return context.Permissions.ToList();
        }

        public async Task<List<RolePermission>> GetRolePermissions()
        {
            return context.RolePermissions.ToList();
        }
    }
}
