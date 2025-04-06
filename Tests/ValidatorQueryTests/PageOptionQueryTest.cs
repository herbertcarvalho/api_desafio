using Backend.Erp.Skeleton.Domain.Extensions;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;

namespace Tests.ValidatorQueryTests
{
    public class PageOptionQueryTest : BaseValidators, ITest
    {
        private readonly string PAGE_NUMBER_INVALID_MESSAGE = "Número da página inválido";
        private readonly string PAGE_SIZE_INVALID_MESSAGE = "Os valores válidos são 10,25,50,100";

        private PageOption query = new();

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("BaseIdQueryTest");
            await ExecuteAssert(PageEqualsZero(), report, "PageEqualsZero");
            await ExecuteAssert(PageLesserThanZero(), report, "PageLesserThanZero");
            await ExecuteAssert(PageSizeEqualsZero(), report, "PageSizeEqualsZero");
            await ExecuteAssert(PageSizeLesserThanZero(), report, "PageSizeLesserThanZero");
        }

        [Test]
        private async Task PageEqualsZero()
        {
            query.Page = 0;
            var errors = ValidateModel(query);

            CustomAssert.Contains(PAGE_NUMBER_INVALID_MESSAGE, errors);
        }

        [Test]
        private async Task PageLesserThanZero()
        {
            query.Page = -1;
            var errors = ValidateModel(query);

            CustomAssert.Contains(PAGE_NUMBER_INVALID_MESSAGE, errors);
        }

        [Test]
        private async Task PageSizeEqualsZero()
        {
            query.PageSize = 0;
            var errors = ValidateModel(query);

            CustomAssert.Contains(PAGE_SIZE_INVALID_MESSAGE, errors);
        }

        [Test]
        private async Task PageSizeLesserThanZero()
        {
            query.PageSize = 1;
            var errors = ValidateModel(query);

            CustomAssert.Contains(PAGE_SIZE_INVALID_MESSAGE, errors);
        }
    }
}
