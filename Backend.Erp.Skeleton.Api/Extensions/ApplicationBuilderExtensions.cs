using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Erp.Skeleton.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
                options.EnableFilter();
                options.DisplayRequestDuration();
            });
        }
    }
}