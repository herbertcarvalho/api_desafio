using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.Validators.Category;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Backend.Erp.Skeleton.Application.Validators.Category.CategoriesConstants;
using static Tests.TestUtils;

namespace Tests.ValidatorQueryTests.Category
{
    public class UpdateCategoryRequestValidatorTest() : BaseValidators, ITest
    {
        private string NAME_EMPTY_MESSAGE = NotEmptyMessage(name);
        private string NAME_NULL_MESSAGE = NotNullMessage(name);
        private string NAME_INVALID_MESSAGE = StringLesserThanInput(name, 100, alphaNumeric: true);

        private UpdateCategoryRequest request = new()
        {
        };

        private readonly UpdateCategoryRequestValidator _validator = new();

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("UpdateCategoryRequestValidatorTest");
            await ExecuteAssert(NameNull, report);
            await ExecuteAssert(NameEmpty, report);
            await ExecuteAssert(NameContainsSymbols, report);
            await ExecuteAssert(NameMoreThan100Characters, report);
        }

        [Test]
        private void NameNull()
        {
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(NAME_NULL_MESSAGE, response);
        }

        [Test]
        private void NameEmpty()
        {
            request.Name = string.Empty;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(NAME_EMPTY_MESSAGE, response);
        }

        [Test]
        private void NameContainsSymbols()
        {
            request.Name = "323232##@@!";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(NAME_INVALID_MESSAGE, response);
        }

        [Test]
        private void NameMoreThan100Characters()
        {
            request.Name = GenerateFixedLengthString(101);
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(NAME_INVALID_MESSAGE, response);
        }
    }
}
