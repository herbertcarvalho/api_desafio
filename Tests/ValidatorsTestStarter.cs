using Tests.ValidatorQueryTests;
using Tests.ValidatorQueryTests.Authorization;
using Tests.ValidatorQueryTests.Category;
using Tests.ValidatorQueryTests.Product;
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
            await new UpdateCategoryRequestValidatorTest().ExecuteTestsAsync(report);
            await new CreateProductRequestValidatorTest().ExecuteTestsAsync(report);
            await new UpdateProductRequestValidatorTest().ExecuteTestsAsync(report);

            WriteTextCmd("Starting Query Tests");
            await new GetCategoriesFilteredQueryTest().ExecuteTestsAsync(report);
            await new UpdateCategoryQueryTest().ExecuteTestsAsync(report);
            await new DeleteCategoryQueryTest().ExecuteTestsAsync(report);
            await new BaseIdQueryTest().ExecuteTestsAsync(report);
            await new PageOptionQueryTest().ExecuteTestsAsync(report);
            await new DeleteProductQueryTest().ExecuteTestsAsync(report);
            await new UpdateProductQueryTest().ExecuteTestsAsync(report);
            await new GetProductsQueryTest().ExecuteTestsAsync(report);
        }
    }
}
