using Tests.ValidatorQueryTests.Authorization;
using Tests.ValidatorQueryTests.Category;
using static Tests.TestUtils;

namespace Tests
{
    public class ValidatorsTestStarter()
    {
        public static async Task ExecuteValidatorsTests(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("Starting Validators Tests");
            await new LoginRequestValidatorTest().ExecuteTestsAsync(report);
            await new RegisterUserRequestValidatorTest().ExecuteTestsAsync(report);
            await new CreateCategoryRequestValidatorTest().ExecuteTestsAsync(report);
        }
    }
}
