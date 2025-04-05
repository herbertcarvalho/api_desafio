using Backend.Erp.Skeleton.Api.Extensions;
using Backend.Erp.Skeleton.Api.Middleware;
using Backend.Erp.Skeleton.Application.DependencyInjection;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Backend.Erp.Skeleton.Infrastructure.Extensions;
using Backend.Erp.Skeleton.Infrastructure.Roles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

var hostUrl = "http://0.0.0.0:80";

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.WebHost.UseUrls(hostUrl).ConfigureServices((context, services) =>
{
    var configuration = context.Configuration;
    builder.Services.AddValidation();
    builder.Services.AddApplicationLayer();
    builder.Services.AddPersistenceContexts(builder.Configuration);
    builder.Services.AddIdentity(builder.Configuration);
    builder.Services.AddMongoDb();
    builder.Services.AddRepositories();
    builder.Services.AddServices();
    builder.Services.AddHelperServices();
    builder.Services.AddControllers();
    builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
    builder.Services.AddEssentials();
    builder.Services.AddCors();
    builder.Services.AddRefit(configuration);
    builder.Services.AddAmazonS3(configuration);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("app");

    try
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_POSTGRES");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        Console.WriteLine(connectionString);

        logger.LogInformation("Application starting");
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        logger.LogInformation("Database migration success");
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        Console.WriteLine(connectionString);

    }
    catch (Exception ex)
    {
        logger.LogError("Error during database migration:" + ex.ToString());
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        Console.WriteLine(ex.ToString());
    }

    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();
}

app.ConfigureSwagger();

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

await CreateRoles(app.Services);

app.Run();

static async Task CreateRoles(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    foreach (var roleName in Role.Roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            var identityRole = new IdentityRole<int>(roleName);
            await roleManager.CreateAsync(identityRole);
            await roleManager.AddClaimAsync(identityRole, new Claim(ClaimTypes.Role, roleName));
        }
    }
}