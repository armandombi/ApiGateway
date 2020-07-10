using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaymentApi.Core.Interfaces;
using PaymentApi.Infrastructure.Services;

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
                .AddScoped<IAcquiringBankService, AcquiringBankService>();

        }
    }
}
