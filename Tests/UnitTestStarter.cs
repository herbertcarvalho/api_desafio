using Microsoft.Extensions.DependencyInjection;
using Tests.UnitTest.Category;
using Tests.UnitTest.Product;
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
            //CATEGORY
            await new CreateCategoryCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
            await new DeleteCategoryCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
            await new GetCategoriesFilteredCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
            await new UpdateCategoryCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);

            //PRODUCTS
            await new CreateProductCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
            await new UpdateProductCommandUnitTest(ServiceProvider).ExecuteTestsAsync(report);
        }
    }
}
