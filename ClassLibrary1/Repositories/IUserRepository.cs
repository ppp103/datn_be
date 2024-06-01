using datn.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Domain
{
    public interface IUserRepository
    {
        Task<RegistrationResponse> ResgisterUserAsync(RegisterUserDto registerUserDto);
        Task<LoginResponse> LoginUserAsync(LoginUserDto loginUserDto);
        Task<PagedList<UserDto>> GetAllUserPaggingAsync(int page, int pageSize, string keyWord);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<int> UpdateUserAsync(int id, User user);
        Task<int> UpdateUserStatusAsync(int id, int isActive);
        Task<UpdatePasswordResponse> UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto);
        Task<UpdatePasswordResponse> UpdateEmailAsync(UpdateEmailDto updateEmailDto);
        Task<int> DeleteAsync(int id);
    }
}
