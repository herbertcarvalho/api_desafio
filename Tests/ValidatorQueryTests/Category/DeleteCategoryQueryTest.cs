using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;
namespace Tests.ValidatorQueryTests.Category
{
    public class DeleteCategoryQueryTest : BaseValidators, ITest
    {
        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("DeleteCategoryQueryTest");
        }
    }
}
