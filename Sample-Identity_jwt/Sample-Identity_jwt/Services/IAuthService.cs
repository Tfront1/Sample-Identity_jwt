using Sample_Identity_jwt.Models;

namespace Sample_Identity_jwt.Services
{
    public interface IAuthService
    {
        string GenerateToken(LoginUserDto loginUserDto);
        Task<bool> Login(LoginUserDto loginUserDto);
        Task<bool> Register(LoginUserDto loginUserDto);
    }
}
