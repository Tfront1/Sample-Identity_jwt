using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Services
{
    public interface IAdministrationService
    {
        Task<List<IdentityUser>> GetUsers();
        Task<List<IdentityRole>> GetRoles();
        Task<List<Permission>> GetPermissions();
        Task<List<RolePermission>> GetRolePermissions();
    }
}
