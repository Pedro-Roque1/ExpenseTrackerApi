using ExpenseTrackerApi.Models.Dtos;
using ExpenseTrackerApi.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackerApi.Services
{
     
    public class AuthService
    {
        private IConfiguration  _configuration;
        private IUserRepository _userService;
        private SignInManager<User> _signInManager;

        public AuthService(IConfiguration configuration, IUserRepository user)
        {
            _configuration = configuration;
            _userService = user;
        }

        public async Task<string> LoginUser(UserLoginRequest userDto)
        {
            var user = await _userService.FindUserByUserName(userDto.Login);
            if (user == null)
                throw new ApplicationException("User Not Found.");

            var checkPassword = BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password);
            if (!checkPassword)
                throw new ApplicationException("Invalid password.");

            return GeneretateJWT(user);
        }

        private string GeneretateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.UserData, user.UserName!)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
