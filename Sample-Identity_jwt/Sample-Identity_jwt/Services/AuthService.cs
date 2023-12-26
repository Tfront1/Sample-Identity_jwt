using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sample_Identity_jwt.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample_Identity_jwt.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public string GenerateToken(LoginUserDto loginUserDto)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,loginUserDto.UserName),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));

            var signingCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience: configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public async Task<bool> Login(LoginUserDto loginUserDto)
        {
            var identityUser = await userManager.FindByEmailAsync(loginUserDto.UserName);
            if(identityUser is null)
            {
                return false;
            }
            return await userManager.CheckPasswordAsync(identityUser, loginUserDto.Password);
        }

        public async Task<bool> Register(LoginUserDto loginUserDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = loginUserDto.UserName,
                Email = loginUserDto.UserName,
            };
            var user = await userManager.CreateAsync(identityUser,loginUserDto.Password);
            return user.Succeeded;
        }
    }
}
