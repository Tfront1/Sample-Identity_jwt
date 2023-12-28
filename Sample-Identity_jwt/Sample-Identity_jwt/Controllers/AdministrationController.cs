using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample_Identity_jwt.Models;
using Sample_Identity_jwt.PermissionModule;
using Sample_Identity_jwt.Services;

namespace Sample_Identity_jwt.Controllers
{
    [HasPermission(Permissions.Administration)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationService administrationService;
        public AdministrationController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await administrationService.GetUsers());
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await administrationService.GetRoles());
        }

        [HttpGet("GetPermissions")]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await administrationService.GetPermissions());
        }

        [HttpGet("GetRolePermissions")]
        public async Task<IActionResult> GetRolePermissions()
        {
            return Ok(await administrationService.GetRolePermissions());
        }

    }
}
