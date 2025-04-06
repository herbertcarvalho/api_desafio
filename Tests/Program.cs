
using Backend.Erp.Skeleton.Application.DependencyInjection;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Backend.Erp.Skeleton.Infrastructure.Extensions;
using Backend.Erp.Skeleton.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Tests;
using static Tests.TestUtils;

var services = new ServiceCollection();

services.AddValidation();
services.AddApplicationLayer();
services.AddMongoDb();
services.AddRepositories();
services.AddServices();
services.AddHelperServices();
services.AddControllers();
services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
services.AddCors();

services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
services.AddDbContext<ApplicationDbContext>(
                 options => options
                 .UseLazyLoadingProxies()
                 .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                 .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning)
                                                        .Ignore(RelationalEventId.PendingModelChangesWarning))
                 .UseNpgsql("Host=localhost;Port=5432;Database=tests-database;Username=myuser;Password=mypassword;",
                 b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
             );

var serviceProvider = services.BuildServiceProvider();

//APAGA O TXT DOS LOGS
ResetText();
var report = new Tuple<List<int>, List<string>>(new List<int>() { 0, 0 }, new List<string>());

using (var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>())
{
    WriteTextCmd("Starting Migration");
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Database migration successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error migrating database: {ex.Message}");
    }

    var seedData = new IntegrationDataTestStarter(serviceProvider);
    await seedData.ExecuteSeedDataTests(report);

    await ValidatorsTestStarter.ExecuteValidatorsTests(report);

    var unitTest = new UnitTestStarter(serviceProvider);
    await unitTest.ExecuteUnitTests(report);

    WriteTextCmd("Starting Drop Database");
    try
    {
        dbContext.Database.EnsureDeleted();
        Console.WriteLine("Database remove successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error removing database: {ex.Message}");
    }

    Console.WriteLine("");
    Console.WriteLine("TESTS IS COMPLETED");
    Console.WriteLine("TESTS STATUS");
    Console.WriteLine($"################# {report.Item1[1]} tests is done #################");
    Console.WriteLine($"################# {report.Item1[0]} tests is wrong #################");
    Console.WriteLine("");

    if (report.Item1[0].Equals(0))
        Console.WriteLine("All tests have completed and success , congrats!!");
    else
    {
        Console.WriteLine("Your tests is NOT completed with success ,please check the archive TestsFailed.txt");
        Environment.Exit(1);
    }
}