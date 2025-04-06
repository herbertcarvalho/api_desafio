using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;

namespace Tests.UnitTest.Product
{
    public class GetProductsFilteredCommandUnitTest : BaseMediator, ITest
    {
        public GetProductsFilteredCommandUnitTest(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("GetProductsFilteredCommandUnitTest");

        }
    }
}
