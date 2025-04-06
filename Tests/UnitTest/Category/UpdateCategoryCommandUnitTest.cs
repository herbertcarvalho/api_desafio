using Backend.Erp.Skeleton.Application.Commands.Category;
using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.UnitTest.Category
{
    public class UpdateCategoryCommandUnitTest : BaseMediator, ITest
    {
        private string CATEGORY_NOT_FOUND = "Não foi possível encontrar a categoria selecionada.";
        private string CATEGORY_ALREADY_REGISTRED = "Essa categoria já foi cadastrada.";

        private readonly Mock<ICategoriesRepository> _categoriesRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly UpdateCategoryCommandHandler _commandHandler;

        UpdateCategoryRequest request = new()
        {
            Name = "alohomora"
        };

        UpdateCategoryQuery query = new() { Id = 1 };

        Categories category = new Categories()
        {
            Id = 1,
        };

        UserClaim claim = new UserClaim();

        public UpdateCategoryCommandUnitTest(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _categoriesRepository = new Mock<ICategoriesRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _commandHandler = new UpdateCategoryCommandHandler(
                _categoriesRepository.Object,
                _unitOfWork.Object
                );
        }

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("UpdateCategoryCommandUnitTest");
            await ExecuteAssert(CategoryNotFound(), report, "CategoryNotFound");
            await ExecuteAssert(CategoryAlreadyRegistred(), report, "CategoryAlreadyRegistred");
        }

        [Test]
        private async Task CategoryNotFound()
        {
            _categoriesRepository
                .Setup(x => x.GetByIdAsync(query.Id))
                .ReturnsAsync((Categories)null);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new UpdateCategoryCommand(claim, query, request), default));
            Assert.AreEqual(message, CATEGORY_NOT_FOUND);
        }

        [Test]
        private async Task CategoryAlreadyRegistred()
        {
            _categoriesRepository
                .Setup(x => x.GetByIdAsync(query.Id))
                .ReturnsAsync(category);

            _categoriesRepository
                .Setup(x => x.Any(request.Name))
                .ReturnsAsync(true);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new UpdateCategoryCommand(claim, query, request), default));
            Assert.AreEqual(message, CATEGORY_ALREADY_REGISTRED);
        }
    }
}
