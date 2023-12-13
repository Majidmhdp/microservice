using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace MicroService.GatewaySolution.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        //public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        //{
        //    var secret = Configuration.GetValue<string>("ApiSettings:JwtOptions:Secret");
        //    var issuer = Configuration.GetValue<string>("ApiSettings:JwtOptions:Issuer");
        //    var audience = Configuration.GetValue<string>("ApiSettings:JwtOptions:Audience");

        //    var key = Encoding.ASCII.GetBytes(secret);

        //    builder.services.AddAuthentication(x =>
        //    {
        //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(x =>
        //    {
        //        x.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = true,
        //            ValidIssuer = issuer,
        //            ValidateAudience = true,
        //            ValidAudience = audience
        //        };
        //    });
        //}
    }
}
