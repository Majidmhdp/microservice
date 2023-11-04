using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Models.Utility;
using MicroService.Web.Service;
using MicroService.Web.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MicroService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ICouponService, CouponService>();
            services.AddHttpClient<IAuthService, AuthService>();

            SD.CouponApiBase = Configuration.GetValue<string>("ServiceUrls:CouponApi");
            SD.AuthApiBase = Configuration.GetValue<string>("ServiceUrls:AuthApi");

            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.ExpireTimeSpan = TimeSpan.FromHours(10);
                    option.LoginPath = "/auth/login";
                    option.AccessDeniedPath = "/auth/accessdenied";

                    //o.Cookie.Name = options.CookieName;
                    //o.Cookie.Domain = options.CookieDomain;
                    //o.SlidingExpiration = true;
                    //o.ExpireTimeSpan = options.CookieLifetime;
                    //o.TicketDataFormat = ticketFormat;
                    //o.CookieManager = new CustomChunkingCookieManager();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

           
        }
    }
}
