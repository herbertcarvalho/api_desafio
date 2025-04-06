using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.Validators.Product;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Backend.Erp.Skeleton.Application.Validators.Product.ProductsConstants;
using static Tests.TestUtils;

namespace Tests.ValidatorQueryTests.Product
{
    public class UpdateProductRequestValidatorTest() : BaseValidators, ITest
    {
        private string PRICE_INVALID = GreaterThanZeroMessage(price);

        private string NAME_EMPTY_MESSAGE = NotEmptyMessage(name);
        private string NAME_NULL_MESSAGE = NotNullMessage(name);
        private string NAME_INVALID_MESSAGE = StringLesserThanInput(name, 100, alphaNumeric: true);

        private UpdateProductRequest request = new()
        {

        };

        private readonly UpdateProductRequestValidator _validator = new();
        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("UpdateProductRequestValidatorTest");
            await ExecuteAssert(PriceEqualsZero, report);
            await ExecuteAssert(PriceLesserThanZero, report);
            await ExecuteAssert(NameNull, report);
            await ExecuteAssert(NameEmpty, report);
            await ExecuteAssert(NameContainsSymbols, report);
            await ExecuteAssert(NameMoreThan100Characters, report);
        }

        [Test]
        private void PriceEqualsZero()
        {
            request.Price = 0;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PRICE_INVALID, response);
        }

        [Test]
        private void PriceLesserThanZero()
        {
            request.Price = -1;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PRICE_INVALID, response);
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
