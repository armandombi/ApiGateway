using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Settings;
using PaymentApi.Infrastructure.Services;
using System;
using System.Net.Http;

namespace PaymentApi.Extensions
{
    /// <summary>
    /// Extension to handle Scoped services
    /// </summary>
    public static class ScopedExtension
    {
        /// <summary>
        /// Adding Scoped services to application startup
        /// </summary>
        /// <param name="services">The API service collection</param>
        public static void AddScopedServices(this IServiceCollection services)
        {
            services
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<IAcquiringBankService>(serv =>
               {
                   var settings = serv.GetRequiredService<IOptions<BankApiSettings>>().Value;
                   return new AcquiringBankService(new HttpClient { BaseAddress = new Uri(settings.BaseUrl ?? ""), Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds) });
               });

        }
    }
}
