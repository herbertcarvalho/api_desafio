using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Tests.TestUtils;

namespace Tests.ValidatorQueryTests.Product
{
    public class GetProductsQueryTest : BaseValidators, ITest
    {
        private readonly string ID_COMPANY_INVALID = GreaterThanZeroMessage("IdCompany");
        private readonly string ID_CATEGORY_INVALID = GreaterThanZeroMessage("IdCategory");
        private readonly string MIN_PRICE_INVALID = GreaterThanZeroMessage("MinPrice");
        private readonly string MAX_PRICE_INVALID = GreaterThanZeroMessage("MaxPrice");

        private GetProductsQuery query = new();

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("GetProductsQueryTest");
            await ExecuteAssert(IdCompanyEqualsZero(), report, "IdCompanyEqualsZero");
            await ExecuteAssert(IdCompanyLesserThanZero(), report, "IdCompanyLesserThanZero");
            await ExecuteAssert(IdCategoryEqualsZero(), report, "IdCategoryEqualsZero");
            await ExecuteAssert(IdCategoryLesserThanZero(), report, "IdCategoryLesserThanZero");
            await ExecuteAssert(MinPriceEqualsZero(), report, "MinPriceEqualsZero");
            await ExecuteAssert(MinPriceLesserThanZero(), report, "MinPriceLesserThanZero");
            await ExecuteAssert(MaxPriceEqualsZero(), report, "MaxPriceEqualsZero");
            await ExecuteAssert(MaxPriceLesserThanZero(), report, "MaxPriceLesserThanZero");
        }

        [Test]
        private async Task IdCompanyEqualsZero()
        {
            query.IdCompany = 0;
            var errors = ValidateModel(query);

            CustomAssert.Contains(ID_COMPANY_INVALID, errors);
        }

        [Test]
        private async Task IdCompanyLesserThanZero()
        {
            query.IdCompany = -1;
            var errors = ValidateModel(query);

            CustomAssert.Contains(ID_COMPANY_INVALID, errors);
        }

        [Test]
        private async Task IdCategoryEqualsZero()
        {
            query.IdCategory = 0;
            var errors = ValidateModel(query);

            CustomAssert.Contains(ID_CATEGORY_INVALID, errors);
        }

        [Test]
        private async Task IdCategoryLesserThanZero()
        {
            query.IdCategory = -1;
            var errors = ValidateModel(query);

            CustomAssert.Contains(ID_CATEGORY_INVALID, errors);
        }

        [Test]
        private async Task MinPriceEqualsZero()
        {
            query.MinPrice = 0;
            var errors = ValidateModel(query);

            CustomAssert.Contains(MIN_PRICE_INVALID, errors);
        }

        [Test]
        private async Task MinPriceLesserThanZero()
        {
            query.MinPrice = -1;
            var errors = ValidateModel(query);

            CustomAssert.Contains(MIN_PRICE_INVALID, errors);
        }

        [Test]
        private async Task MaxPriceEqualsZero()
        {
            query.MaxPrice = 0;
            var errors = ValidateModel(query);

            CustomAssert.Contains(MAX_PRICE_INVALID, errors);
        }

        [Test]
        private async Task MaxPriceLesserThanZero()
        {
            query.MaxPrice = -1;
            var errors = ValidateModel(query);

            CustomAssert.Contains(MAX_PRICE_INVALID, errors);
        }
    }
}
