using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Models;
using MicroService.Web.Models.Utility;
using MicroService.Web.Service.IService;

namespace MicroService.Web.Service
{
    public class OrderService : IOrderService
    {

        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Date = cartDto,
                Url = SD.OrderApiBase + "/api/order/createOrder"
            });
        }

        public async Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Date = stripeRequestDto,
                Url = SD.OrderApiBase + "/api/order/CreateStripeSession"
            });
        }

        public async Task<ResponseDto> ValidateStripeSession(int orderHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Date = orderHeaderId,
                Url = SD.OrderApiBase + "/api/order/ValidateStripeSession"
            });
        }
    }
}
