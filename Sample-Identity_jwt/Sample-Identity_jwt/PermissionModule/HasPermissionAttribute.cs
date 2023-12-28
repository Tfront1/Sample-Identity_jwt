using Microsoft.AspNetCore.Authorization;

namespace Sample_Identity_jwt.PermissionModule
{
    public class HasPermissionAttribute: AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission)
            :base(policy: permission.ToString())
        {
        }
    }
}
