using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Models;
using MicroService.Web.Models.Utility;
using MicroService.Web.Service.IService;

namespace MicroService.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Date = registrationRequestDto,
                Url = SD.AuthApiBase + "/api/auth/assignrole"
            });
        }

        public async Task<ResponseDto> LoginAsunc(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Date = loginRequestDto,
                    Url = SD.AuthApiBase + "/api/auth/login"
                },
                withBearer: false);
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.POST,
                    Date = registrationRequestDto,
                    Url = SD.AuthApiBase + "/api/auth/register"
                },
                withBearer: false);
        }

      
    }
}
