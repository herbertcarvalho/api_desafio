using Backend.Erp.Skeleton.Application.Behaviors;
using Backend.Erp.Skeleton.Application.Helpers;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
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

        public static IServiceCollection AddQueryServices(this IServiceCollection services)
        {
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
    }
}