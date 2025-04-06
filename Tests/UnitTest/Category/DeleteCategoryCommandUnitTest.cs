using Backend.Erp.Skeleton.Application.Commands.Category;
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
    public class DeleteCategoryCommandUnitTest : BaseMediator, ITest
    {
        private string NOT_FOUND_CATEGORY = "Não foi possível encontrar a categoria selecionada.";
        private string CATEGORY_LINKED_PRODUCTS = "Não é possível apagar essa categoria pois a mesma se encontra vinculada à produtos.";

        private readonly Mock<ICategoriesRepository> _categoriesRepository;
        private readonly Mock<IProductsRepository> _productsRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly DeleteCategoryCommandHandler _commandHandler;

        DeleteCategoryQuery query = new()
        {
            Id = 1
        };

        Categories category = new Categories()
        {
            Id = 1,
        };

        public DeleteCategoryCommandUnitTest(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _categoriesRepository = new Mock<ICategoriesRepository>();
            _productsRepository = new Mock<IProductsRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _commandHandler = new DeleteCategoryCommandHandler(
                _categoriesRepository.Object,
                _unitOfWork.Object,
                _productsRepository.Object
                );
        }

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("DeleteCategoryCommandUnitTest");
            await ExecuteAssert(CategoryNotFound(), report, "CategoryNotFound");
            await ExecuteAssert(CategoryLinkedToProducts(), report, "CategoryLinkedToProducts");
        }

        [Test]
        private async Task CategoryNotFound()
        {
            _categoriesRepository
                .Setup(x => x.GetByIdAsync(query.Id))
                .ReturnsAsync((Categories)null);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new DeleteCategoryCommand(query), default));
            Assert.AreEqual(message, NOT_FOUND_CATEGORY);
        }

        [Test]
        private async Task CategoryLinkedToProducts()
        {
            _categoriesRepository
                .Setup(x => x.GetByIdAsync(query.Id))
                .ReturnsAsync(category);

            _productsRepository
                .Setup(x => x.Any(category.Id))
                .ReturnsAsync(true);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new DeleteCategoryCommand(query), default));
            Assert.AreEqual(message, CATEGORY_LINKED_PRODUCTS);
        }
    }
}
