using Backend.Erp.Skeleton.Application.Commands.Category;
using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Domain.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.UnitTest.Category
{
    public class CreateCategoryCommandUnitTest : BaseMediator, ITest
    {
        private string CATEGORY_ALREADY_REGISTRED = "Essa categoria já foi cadastrada.";

        private readonly Mock<ICategoriesRepository> _categoriesRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CreateCategoryCommandHandler _commandHandler;

        CreateCategoryRequest request = new()
        {
            Name = "alohomora"
        };

        UserClaim claim = new UserClaim();

        public CreateCategoryCommandUnitTest(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _categoriesRepository = new Mock<ICategoriesRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _commandHandler = new CreateCategoryCommandHandler(
                _categoriesRepository.Object,
                _unitOfWork.Object
                );
        }

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("CreateCategoryCommandUnitTest");
            await ExecuteAssert(CategoryAlreadyRegistred(), report, "CategoryAlreadyRegistred()");
        }

        [Test]
        private async Task CategoryAlreadyRegistred()
        {
            _categoriesRepository
                .Setup(x => x.Any(request.Name))
                .ReturnsAsync(true);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new CreateCategoryCommand(claim, request), default));
            Assert.AreEqual(message, CATEGORY_ALREADY_REGISTRED);
        }
    }
}
