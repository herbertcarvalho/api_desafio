using Microsoft.Extensions.DependencyInjection;
using Tests.UnitTest.Category;
using static Tests.TestUtils;

namespace Tests
{
    public class UnitTestStarter
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitTestStarter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected IServiceProvider ServiceProvider => _serviceProvider.GetRequiredService<IServiceProvider>();

        public async Task ExecuteUnitTests(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("Starting Unit Tests");
            await new CreateCategoryCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
            await new DeleteCategoryCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
            await new GetCategoriesFilteredCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
            await new UpdateCategoryCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
        }
    }
}
