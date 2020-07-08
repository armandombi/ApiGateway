using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PaymentApi.Extensions.Swagger
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerOptions(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });
        }

        public static void UseSwaggerGen(this IApplicationBuilder applicationBuilder, IApiVersionDescriptionProvider apiVersion)
        {

            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(
                options =>
                {
                    for (var i = apiVersion.ApiVersionDescriptions.Count - 1; i >= 0; i--)
                    {
                        var description = apiVersion.ApiVersionDescriptions[i];
                        options.SwaggerEndpoint($"swagger/{description.GroupName}/swagger.json", $"Payment API {description.GroupName}");
                        options.RoutePrefix = string.Empty;
                        options.DocExpansion(DocExpansion.None);
                    }
                });
        }

        private static string XmlCommentsFilePath {
            get {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
