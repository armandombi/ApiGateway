using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentApi.Extensions;
using Serilog;

namespace PaymentApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services collection to add to the API</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddConfigurationOptions(Configuration);
            services.AddApiVersioningOptions();
            services.AddMongoDb(Configuration);
            services.AddSecurity();
            services.AddSwaggerOptions();
            services.AddPerformance();
            services.AddScopedServices();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder for the request pipeline</param>
        /// <param name="env">The environment the application is running</param>
        /// <param name="apiVersion">The information for the different api versions</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersion)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSerilogRequestLogging();
            app.UsePerformance();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerGen(apiVersion);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}