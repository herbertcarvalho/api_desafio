using Backend.Erp.Skeleton.Application.DTOs;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Tests.TestUtils;

namespace Tests.ValidatorQueryTests
{
    public class BaseIdQueryTest : BaseValidators, ITest
    {
        private readonly string ID_INVALID = GreaterThanZeroMessage("Id");

        private BaseIdQuery query = new();

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("BaseIdQueryTest");
            await ExecuteAssert(IdEqualsZero(), report, "IdEqualsZero");
            await ExecuteAssert(IdLesserThanZero(), report, "IdLesserThanZero");
        }

        [Test]
        private async Task IdEqualsZero()
        {
            query.Id = 0;
            var errors = ValidateModel(query);

            CustomAssert.Contains(ID_INVALID, errors);
        }

        [Test]
        private async Task IdLesserThanZero()
        {
            query.Id = -1;
            var errors = ValidateModel(query);

            CustomAssert.Contains(ID_INVALID, errors);
        }
    }
}
