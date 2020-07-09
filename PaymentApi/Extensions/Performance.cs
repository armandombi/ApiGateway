using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System.Linq;

namespace PaymentApi.Extensions
{
    internal static class PerformanceExtension
    {
        internal static void AddPerformance(this IServiceCollection services)
        {

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });


            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        internal static void UsePerformance(this IApplicationBuilder app)
        {
            app.UseResponseCompression();
        }
    }
}
