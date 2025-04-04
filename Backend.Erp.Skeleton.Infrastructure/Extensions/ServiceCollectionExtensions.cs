using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Backend.Erp.Skeleton.Infrastructure.Interfaces;
using Backend.Erp.Skeleton.Infrastructure.Repositories;
using Backend.Erp.Skeleton.Infrastructure.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Backend.Erp.Skeleton.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            AddDbContext(services, configuration);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }

        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultIdentity<IdentityUser<int>>()
                .AddRoles<IdentityRole<int>>()
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidAudience = configuration["TokenConfiguration:Audience"],
                     ValidIssuer = configuration["TokenConfiguration:Issuer"],
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
                 });

            services.AddAuthorizationBuilder()
                .AddPolicy(Role.Company, policy => policy.RequireRole(Role.Company))
                .AddPolicy(Role.Client, policy => policy.RequireRole(Role.Client));
        }

        public static void AddMongoDb(this IServiceCollection services)
        {
            services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        }

        [ExcludeFromCodeCoverage]
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                 options => options.UseLazyLoadingProxies()
                    .EnableSensitiveDataLogging()
                    .UseNpgsql(configuration.GetConnectionString("ApplicationConnection"),
                 b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
             );
        }
    }
}