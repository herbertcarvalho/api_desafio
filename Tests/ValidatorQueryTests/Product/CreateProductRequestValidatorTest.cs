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
    public class CreateProductRequestValidatorTest() : BaseValidators, ITest
    {
        private string ID_CATEGORY_INVALID = GreaterThanZeroMessage(idCategory);
        private string PRICE_INVALID = GreaterThanZeroMessage(idCategory);

        private string NAME_EMPTY_MESSAGE = NotEmptyMessage(name);
        private string NAME_NULL_MESSAGE = NotNullMessage(name);
        private string NAME_INVALID_MESSAGE = StringLesserThanInput(name, 100, alphaNumeric: true);

        private string BASE64_INVALID_MESSAGE = Base64Invalid();

        private CreateProductRequest request = new()
        {

        };

        private readonly CreateProductRequestValidator _validator = new();
        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("CreateProductRequestValidatorTest");
            await ExecuteAssert(IdCategoryEqualsZero, report);
            await ExecuteAssert(IdCategoryLesserThanZero, report);
            await ExecuteAssert(PriceEqualsZero, report);
            await ExecuteAssert(PriceLesserThanZero, report);
            await ExecuteAssert(NameNull, report);
            await ExecuteAssert(NameEmpty, report);
            await ExecuteAssert(NameContainsSymbols, report);
            await ExecuteAssert(NameMoreThan100Characters, report);
            await ExecuteAssert(Base64Null, report);
            await ExecuteAssert(Base64Empty, report);
            await ExecuteAssert(Base64InvalidError, report);
        }

        [Test]
        private void IdCategoryEqualsZero()
        {
            request.IdCategory = 0;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(ID_CATEGORY_INVALID, response);
        }

        [Test]
        private void IdCategoryLesserThanZero()
        {
            request.IdCategory = -1;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(ID_CATEGORY_INVALID, response);
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

        [Test]
        private void Base64Null()
        {
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(BASE64_INVALID_MESSAGE, response);
        }

        [Test]
        private void Base64Empty()
        {
            request.Img = string.Empty;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(BASE64_INVALID_MESSAGE, response);
        }

        [Test]
        private void Base64InvalidError()
        {
            request.Img = "askoask";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(BASE64_INVALID_MESSAGE, response);
        }
    }
}
