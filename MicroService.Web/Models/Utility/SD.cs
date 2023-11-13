using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Web.Models.Utility
{
    public class SD // Static Detail
    {
        public static string CouponApiBase { get; set; }
        public static string ProductApiBase { get; set; }

        public static string AuthApiBase { get; set; }

        public static string ShoppingCartApiBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
