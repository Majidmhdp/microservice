using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Services.EmailAPI.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MicroService.Services.EmailAPI.Extention
{
    public static class ApplicatioonBuilderExtentions
    {
        private static IAzureServiceBusConsumer serviceBusConsumer { get; set; }

        public static IApplicationBuilder UseAzureServiceBusComsumer(this IApplicationBuilder app)
        {
            serviceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStop()
        {
            serviceBusConsumer.Stop();
        }

        private static void OnStart()
        {
            serviceBusConsumer.Start();
        }
    }
}
