using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;

namespace Tests.UnitTest.Category
{
    public class GetCategoriesFilteredCommandUnitTest : BaseMediator, ITest
    {

        public GetCategoriesFilteredCommandUnitTest(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("GetCategoriesFilteredCommandUnitTest");
        }
    }
}
