using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using PaymentApi.Core.Helpers;

namespace PaymentApi.Extensions
{
    /// <summary>
    /// Extension to handle security
    /// </summary>
    public static class SecurityExtension
    {
        /// <summary>
        /// Adds API security configuration
        /// </summary>
        /// <param name="services">The API service collection</param>
        public static void AddSecurity(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddScheme<AuthenticationSchemeOptions, ApiAuthenticationHandler>("Bearer", null);
        }
    }
}