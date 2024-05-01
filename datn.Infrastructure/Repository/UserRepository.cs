using datn.Domain;
using datn.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace datn.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly QuestionDbContext _questionDbContext;
        private readonly IConfiguration _configuration;
        public UserRepository(QuestionDbContext questionDbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _questionDbContext = questionDbContext;     

        }
        public async Task<LoginResponse> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var getUser = await _questionDbContext.User.FirstOrDefaultAsync(u => u.UserName == loginUserDto.UserName);
            if (getUser == null)
            {
                return new LoginResponse(false, "Không tìm thấy tên tài khoản");
            }

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, getUser.Password);

            if (checkPassword)
            {
                return new LoginResponse(true, "Đăng nhập thành công", GenerateJWTToken(getUser));
            }
            else
            {
                return new LoginResponse(false, "Đăng nhập thất bại");
            }
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //var userClaims = new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.Email, user.Email)
            //};

            var userClaims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.UserName),
                new Claim("Email", user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> FindUserByUserName(string userName) => await _questionDbContext.User.FirstOrDefaultAsync(x => x.UserName == userName);    

        public async Task<RegistrationResponse> ResgisterUserAsync(RegisterUserDto registerUserDto)
        {
            var getUser = await FindUserByUserName(registerUserDto.UserName);
            if (getUser != null)
            {
                return new RegistrationResponse(false, "Người dùng đã tồn tại");
            }

            _questionDbContext.User.Add(new User()
            {
                UserName = registerUserDto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                Email = registerUserDto.Email,
            });

            await _questionDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Đăng ký thành công");
        }
    }
}
