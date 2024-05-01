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
    }
}
