using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Models.Utility;

namespace MicroService.Web.Models
{
    public class RequestDto
    {
        public SD.ApiType ApiType { get; set; } = SD.ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

        public SD.ContentType ContentType { get; set; } = SD.ContentType.Json;
    }
}
