using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Sample_Identity_jwt.Services;
using System.Security.Claims;

namespace Sample_Identity_jwt.PermissionModule
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            string? userId = context.User.Claims.FirstOrDefault(
                x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out Guid parseUserId)) {
                return;
            }
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            IPermissionService permissionService = scope.ServiceProvider.GetService<IPermissionService>();

            var permissions = await permissionService
                .GetPermissionsAsync(parseUserId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }

        }
    }
}
