using Backend.Erp.Skeleton.Application.Commands.Product;
using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.Services.Interfaces;
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
    public class CreateProductCommandUnitTest : BaseMediator, ITest
    {
        private string USER_NOT_FOUND = "Não foi possível encontrar o usuário.";
        private string COMPANY_NOT_FOUND = "A empresa selecionada não existe.";
        private string CATEGORY_NOT_FOUND = "A categoria selecionada não existe.";
        private string PRODUCT_ALREADY_REGISTRED = "Esse nome já foi utilizado por outro produto.";

        private readonly Mock<IProductsRepository> _productsRepository;
        private readonly Mock<ICompanyRepository> _companyRepository;
        private readonly Mock<ICategoriesRepository> _categoriesRepository;
        private readonly Mock<IPersonsRepository> _personsRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMinIoServices> _minIoServices;
        private readonly CreateProductCommandHandler _commandHandler;

        CreateProductRequest request = new()
        {
            Name = "alohomora"
        };

        UserClaim claim = new UserClaim()
        {
            IdUser = 1
        };

        Persons person = new Persons()
        {
            IdCompany = 3,
        };

        public CreateProductCommandUnitTest(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _productsRepository = new Mock<IProductsRepository>();
            _companyRepository = new Mock<ICompanyRepository>();
            _categoriesRepository = new Mock<ICategoriesRepository>();
            _personsRepository = new Mock<IPersonsRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _minIoServices = new Mock<IMinIoServices>();

            _commandHandler = new CreateProductCommandHandler(
               _productsRepository.Object,
               _unitOfWork.Object,
               _companyRepository.Object,
               _categoriesRepository.Object,
               _personsRepository.Object,
               _minIoServices.Object
                );
        }

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("CreateProductCommandUnitTest");
            await ExecuteAssert(PersonNotFound(), report, "PersonNotFound");
            await ExecuteAssert(CompanyNotFound(), report, "CompanyNotFound");
            await ExecuteAssert(CategoryNotFound(), report, "CategoryNotFound");
            await ExecuteAssert(ProductAlreadyRegistred(), report, "ProductAlreadyRegistred");
        }

        [Test]
        private async Task PersonNotFound()
        {
            _personsRepository
                .Setup(x => x.GetByIdAsync(claim.IdUser))
                .ReturnsAsync((Persons)null);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new CreateProductCommand(claim, request), default));
            Assert.AreEqual(message, USER_NOT_FOUND);
        }

        [Test]
        private async Task CompanyNotFound()
        {
            _personsRepository
                .Setup(x => x.GetByIdAsync(claim.IdUser))
                .ReturnsAsync(person);

            _companyRepository
                .Setup(x => x.Any(person.IdCompany.Value))
                .ReturnsAsync(false);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new CreateProductCommand(claim, request), default));
            Assert.AreEqual(message, COMPANY_NOT_FOUND);
        }

        [Test]
        private async Task CategoryNotFound()
        {
            _companyRepository
                .Setup(x => x.Any(person.IdCompany.Value))
                .ReturnsAsync(true);

            _categoriesRepository
                .Setup(X => X.Any(request.IdCategory))
                .ReturnsAsync(false);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new CreateProductCommand(claim, request), default));
            Assert.AreEqual(message, CATEGORY_NOT_FOUND);
        }

        [Test]
        private async Task ProductAlreadyRegistred()
        {
            _categoriesRepository
                .Setup(X => X.Any(request.IdCategory))
                .ReturnsAsync(true);

            _productsRepository
                .Setup(x => x.Any(request.Name))
                .ReturnsAsync(true);

            var message = await RunUnitTestAsync(_commandHandler.Handle(new CreateProductCommand(claim, request), default));
            Assert.AreEqual(message, PRODUCT_ALREADY_REGISTRED);
        }
    }
}
