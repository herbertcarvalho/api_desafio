using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;

namespace Tests.ValidatorQueryTests.Product
{
    public class DeleteProductQueryTest : BaseValidators, ITest
    {
        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("DeleteProductQueryTest");
        }
    }
}
