using Amazon.Runtime;
using Amazon.S3;
using Backend.Erp.Skeleton.Application.Behaviors;
using Backend.Erp.Skeleton.Application.Helpers;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
using Backend.Erp.Skeleton.Application.Services;
using Backend.Erp.Skeleton.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Backend.Erp.Skeleton.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMinIoServices, MinIoServices>();
            return services;
        }

        public static IServiceCollection AddHelperServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthHelper, AuthHelper>();
            return services;
        }

        public static IServiceCollection AddRefit(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddAmazonS3(this IServiceCollection services, IConfiguration configuration)
        {
            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(configuration.GetSection("AWS")["AWSAccessKeyId"], configuration.GetSection("AWS")["AWSSecretKey"]);
            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonS3>();

            services.AddScoped<IAws3Services, Aws3Services>();

            return services;
        }
    }
}