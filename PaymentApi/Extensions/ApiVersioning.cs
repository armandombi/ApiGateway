﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentApi.Extensions
{
    /// <summary>
    /// Extension to handle Api versioning
    /// </summary>
    public static class ApiVersioningExtension
    {
        /// <summary>
        /// This extension method adds the required setting to configure versioning on the api
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiVersioningOptions(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

        }
    }
}
