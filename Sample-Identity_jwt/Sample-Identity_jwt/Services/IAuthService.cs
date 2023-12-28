using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Services
{
    public interface IAuthService
    {
        string GenerateToken(IdentityUser user);
        Task<IdentityUser> Login(LoginUserDto loginUserDto);
        Task<bool> Register(LoginUserDto loginUserDto);
    }
}
