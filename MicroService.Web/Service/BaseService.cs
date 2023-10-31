using MicroService.Web.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MicroService.Web.Model.Dto;
using MicroService.Web.Models;
using MicroService.Web.Models.Utility;
using Newtonsoft.Json;

namespace MicroService.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol =
                //    //SecurityProtocolType.Tls
                //                                       //| SecurityProtocolType.Tls11
                //                                       //SecurityProtocolType.Tls12
                //                                       SecurityProtocolType.Ssl3;

                HttpClient client = _httpClientFactory.CreateClient("MicroService");
                HttpRequestMessage message = new HttpRequestMessage();
                    //= null;

                message.Headers.Add("Accept", "application/json");

                //token

                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Date != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Date), Encoding.UTF8,
                        "application/json");
                }

                switch (requestDto.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }


                HttpResponseMessage apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto() { IsSuccess = false, Message = "UnAuthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
