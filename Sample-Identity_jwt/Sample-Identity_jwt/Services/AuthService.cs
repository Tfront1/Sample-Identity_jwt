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

        public string GenerateToken(IdentityUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, Role.Admin.Name),
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

        public async Task<IdentityUser> Login(LoginUserDto loginUserDto)
        {
            var identityUser = await userManager.FindByEmailAsync(loginUserDto.UserName);
            if(identityUser is null)
            {
                return null;
            }
            if (!await userManager.CheckPasswordAsync(identityUser, loginUserDto.Password)){
                return null;
            }
            return identityUser;
        }

        public async Task<bool> RegisterAdmin(LoginUserDto loginUserDto)
        {
            IdentityUser identityUser = new IdentityUser
            {
                UserName = loginUserDto.UserName,
                Email = loginUserDto.UserName,
            };
            var result = await userManager.CreateAsync(identityUser,loginUserDto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(identityUser, Role.Admin.Name);
            }

            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(LoginUserDto loginUserDto)
        {
            IdentityUser identityUser = new IdentityUser
            {
                UserName = loginUserDto.UserName,
                Email = loginUserDto.UserName,
            };
            var result = await userManager.CreateAsync(identityUser, loginUserDto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(identityUser, Role.User.Name);
            }

            return result.Succeeded;
        }
    }
}
