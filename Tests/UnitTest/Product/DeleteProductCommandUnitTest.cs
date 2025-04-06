using Backend.Erp.Skeleton.Application.Commands.Product;
using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Tests.TestUtils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.UnitTest.Product
{
    public class DeleteProductCommandUnitTest : BaseMediator, ITest
    {
        private string PRODUCT_NOT_FOUND = "Não foi possível encontrar o produto.";
        private string USER_NOT_FOUND = "Não foi possível encontrar o usuário.";
        private string USER_NOT_IN_COMPANY = "Usuário não possui acesso a empresa.";

        private readonly Mock<IProductsRepository> _productsRepository;
        private readonly Mock<IPersonsRepository> _personsRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly UpdateProductCommandHandler _commandHandler;

        UpdateProductRequest request = new()
        {
            Name = "alohomora"
        };

        UpdateProductQuery query = new()
        {
            Id = 1
        };

        UserClaim claim = new UserClaim()
        {
            IdUser = 1
        };

        Persons person = new Persons()
        {
            IdCompany = 4,
        };

        Products product = new Products()
        {
            IdCompany = 3,
        };

        public DeleteProductCommandUnitTest(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _productsRepository = new Mock<IProductsRepository>();
            _personsRepository = new Mock<IPersonsRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _commandHandler = new UpdateProductCommandHandler(
               _productsRepository.Object,
               _personsRepository.Object,
               _unitOfWork.Object
                );
        }

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("DeleteProductCommandUnitTest");
            await ExecuteAssert(ProductNotFound(), report, "ProductNotFound");
            await ExecuteAssert(CompanyNotFound(), report, "CompanyNotFound");
            await ExecuteAssert(CategoryNotFound(), report, "CategoryNotFound");
        }

        [Test]
        private async Task ProductNotFound()
        {
            _productsRepository
                .Setup(x => x.GetByIdAsync(query.Id))
                .ReturnsAsync((Products)null);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new UpdateProductCommand(claim, query, request), default));
            Assert.AreEqual(message, PRODUCT_NOT_FOUND);
        }

        [Test]
        private async Task CompanyNotFound()
        {
            _productsRepository
                .Setup(x => x.GetByIdAsync(query.Id))
                .ReturnsAsync(product);

            _personsRepository
                .Setup(x => x.GetByIdAsync(claim.IdUser))
                .ReturnsAsync((Persons)null);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new UpdateProductCommand(claim, query, request), default));
            Assert.AreEqual(message, USER_NOT_FOUND);
        }

        [Test]
        private async Task CategoryNotFound()
        {
            _personsRepository
                .Setup(x => x.GetByIdAsync(claim.IdUser))
                .ReturnsAsync(person);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new UpdateProductCommand(claim, query, request), default));
            Assert.AreEqual(message, USER_NOT_IN_COMPANY);
        }
    }
}
