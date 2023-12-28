﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Sample_Identity_jwt.PermissionModule
{
    public class PermissionAuthorizationPolicyProvider: 
        DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(
            IOptions<AuthorizationOptions> options)
            :base(options)
        {
        }
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy != null)
            {
                return policy;
            }

            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();
        }
    }
}
