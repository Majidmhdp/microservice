using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Model;
using MicroService.Web.Model.Dto;
using MicroService.Web.Models;
using MicroService.Web.Models.Utility;
using MicroService.Web.Service.IService;

namespace MicroService.Web.Service
{
    public class CouponService : ICouponService
    {

        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Date = couponDto,
                Url = SD.CouponApiBase + "/api/CouponApi"
            });
        }

        public async Task<ResponseDto> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBase + "/api/CouponApi/" + id
            });
        }

        

        public async Task<ResponseDto> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/CouponApi"
            });
        }

        public async Task<ResponseDto> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/CouponApi/getbycode/" + couponCode
            });
        }

        public async Task<ResponseDto> GetCouponsByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/CouponApi/" + id
            });
        }

       

        public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Date = couponDto,
                Url = SD.CouponApiBase + "/api/CouponApi"
            });
        }

        
    }
}
