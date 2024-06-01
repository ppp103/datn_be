using datn.Application;
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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace datn.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly QuestionDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        public UserRepository(QuestionDbContext userDbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _userDbContext = userDbContext;

        }
        public async Task<LoginResponse> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var getUser = await _userDbContext.User.FirstOrDefaultAsync(u => u.UserName == loginUserDto.UserName && u.IsActive == 1);
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

        private async Task<User> FindUserByUserName(string userName) => await _userDbContext.User.FirstOrDefaultAsync(x => x.UserName == userName);

        public async Task<RegistrationResponse> ResgisterUserAsync(RegisterUserDto registerUserDto)
        {
            var getUser = await FindUserByUserName(registerUserDto.UserName);
            if (getUser != null)
            {
                return new RegistrationResponse(false, "Người dùng đã tồn tại");
            }

            _userDbContext.User.Add(new User()
            {
                UserName = registerUserDto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                Email = registerUserDto.Email,
                IsActive = 1,
            });

            await _userDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Đăng ký thành công");
        }

        public async Task<PagedList<UserDto>> GetAllUserPaggingAsync(int page, int pageSize, string keyword)
        {
            var query = from user in _userDbContext.User
                        where string.IsNullOrEmpty(keyword) ||
                              user.UserName.Contains(keyword) ||
                              user.Email.Contains(keyword)
                        select new UserDto()
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Role = user.Role,
                            IsActive = user.IsActive,
                        };

            var res = await PagedList<UserDto>.CreateAsync(query.OrderByDescending(x => x.Id), page, pageSize);

            return res;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var query = from user in _userDbContext.User
                        where user.Id == id
                        select new UserDto()
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Role = user.Role,
                            IsActive = user.IsActive,
                        };
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateUserAsync(int id, User user)
        {
            return await _userDbContext.User.Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters.
                    SetProperty(m => m.Id, user.Id).
                    SetProperty(m => m.UserName, user.UserName).
                    SetProperty(m => m.Email, user.Email).
                    SetProperty(m => m.ImgLink, user.ImgLink)
                );

        }

        public async Task<int> UpdateUserStatusAsync(int id, int isActive)
        {
            return await _userDbContext.User.Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters.
                    SetProperty(m => m.IsActive, isActive)
            );
        }

        public async Task<int> DeleteAsync(int id)
        {
            var user = await _userDbContext.User.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Domain.SystemException("Người dùng không tồn tại");
            }
            return await _userDbContext.User.Where(model => model.Id == id).ExecuteDeleteAsync();
        }

        public async Task<UpdatePasswordResponse> UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto)
        {
            // Tìm xem có tồn tại user không
            var user = await _userDbContext.User.SingleOrDefaultAsync(x => x.Id == updatePasswordDto.Id);

            if (user == null)
            {
                return new UpdatePasswordResponse(false, "Người dùng không tồn tại");
            }

            // Check xem pass cũ đúng không
            bool checkPassword = BCrypt.Net.BCrypt.Verify(updatePasswordDto.OldPassword, user.Password);

            // Đúng thì update
            if (checkPassword)
            {
                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(updatePasswordDto.NewPassword);
                var res = await _userDbContext.User.Where(x => x.Id == updatePasswordDto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.Password, encryptedPassword));
                if(res > 0) { return new UpdatePasswordResponse(true, "Đổi mật khẩu thành công"); }
            }
            return new UpdatePasswordResponse(false, "Mật khẩu cũ không đúng");

            // Không đúng return error
        }

        public async Task<UpdatePasswordResponse> UpdateEmailAsync(UpdateEmailDto updateEmailDto)
        {
            var user = await _userDbContext.User.SingleOrDefaultAsync(x => x.Id == updateEmailDto.Id);

            if (user == null)
            {
                return new UpdatePasswordResponse(false, "Người dùng không tồn tại");
            }

            // Check xem pass cũ đúng không
            bool checkPassword = BCrypt.Net.BCrypt.Verify(updateEmailDto.Password, user.Password);

            // Đúng thì cập nhật email
            if (checkPassword)
            {
                var res = await _userDbContext.User.Where(x => x.Id == updateEmailDto.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.Email, updateEmailDto.Email));
            }

            return new UpdatePasswordResponse(false, "Mật khẩu không đúng");
        }
    }
}
