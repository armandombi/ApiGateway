using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Services;

namespace PaymentApi.Extensions
{
    public static class ScopedExtension
    {
        public static void AddScopedServices(this IServiceCollection services)
        {
            services
                .AddScoped<IPaymentService, PaymentService>();
            //.AddScoped<IAcquiringBankService, AcquiringBankService>();

        }
    }
}
