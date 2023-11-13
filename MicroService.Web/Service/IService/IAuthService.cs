using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Models;

namespace MicroService.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsunc(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
