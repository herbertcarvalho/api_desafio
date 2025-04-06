using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using NUnit.Framework;
using Tests.Interfaces;
using static Tests.TestUtils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.IntegrationTest
{
    public class ProductsIntegrationDataTest(
        IProductsRepository productsRepository,
        IUnitOfWork unitOfWork) : ITest
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("ProductsIntegrationDataTest");
            await MapProduct();
            await ExecuteAssert(ExecutePostProduct(), report, "ExecutePostProduct");
            await ExecuteAssert(AnyByIdCategory(), report, "AnyByIdCategory");
            await ExecuteAssert(AnyByImage(), report, "AnyByImage");
            await ExecuteAssert(AnyByName(), report, "AnyByName");
            await ExecuteAssert(GetFiltered(), report, "GetFiltered");
            await ExecuteAssert(UpdateProduct(), report, "UpdateProduct");
            await ExecuteAssert(DeleteProduct(), report, "DeleteProduct");
        }

        private Products newProduct = new();
        private const bool STATUS = true;
        private const string IMAGE = "dGVzdGU=";
        private const string NAME = "Arroz";
        private const decimal PRICE = 65.98M;

        private const bool STATUS_AUX = true;
        private const string IMAGE_AUX = "ZXhlbXBsbw==";
        private const string NAME_AUX = "Feijão";
        private const decimal PRICE_AUX = 25M;

        private async Task MapProduct()
        {
            await _productsRepository.AddAsync(new Products()
            {
                IdCompany = 1,
                IdCategory = 1,
                Status = true,
                Image = "dGVzdGU=",
                Name = "Products mapping",
                Price = 15.25M,
            });

            await _unitOfWork.SaveChangesAsync();
        }

        [Test]
        private async Task ExecutePostProduct()
        {
            var product = await _productsRepository.AddAsync(new Products()
            {
                IdCompany = 1,
                IdCategory = 1,
                Status = STATUS,
                Image = IMAGE,
                Name = NAME,
                Price = PRICE,
            });

            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(product.Image, IMAGE);
            Assert.AreEqual(product.Status, STATUS);
            Assert.AreEqual(product.Name, NAME);
            Assert.AreEqual(product.Price, PRICE);

            newProduct = product;
        }

        [Test]
        private async Task AnyByIdCategory()
        {
            var exits = await _productsRepository.Any(newProduct.IdCategory);

            Assert.AreEqual(exits, true);
        }

        [Test]
        private async Task AnyByImage()
        {
            var exits = await _productsRepository.AnyImage(newProduct.Image);

            Assert.AreEqual(exits, true);
        }

        [Test]
        private async Task AnyByName()
        {
            var exits = await _productsRepository.Any(newProduct.Name);

            Assert.AreEqual(exits, true);
        }

        [Test]
        private async Task GetFiltered()
        {
            var products = await _productsRepository.GetFiltered(null, null, null, null, pageOption: new PageOption()
            {
                Page = 1,
                PageSize = 100
            });

            var selectedProducts = products.Data.Last();
            if (selectedProducts is null)
                Assert.Fail("Não deveria ser null.");

            Assert.AreEqual(selectedProducts.Image, IMAGE);
            Assert.AreEqual(selectedProducts.Status, STATUS);
            Assert.AreEqual(selectedProducts.Name, NAME);
            Assert.AreEqual(selectedProducts.Price, PRICE);
        }

        [Test]
        private async Task UpdateProduct()
        {
            newProduct.Status = STATUS_AUX;
            newProduct.Image = IMAGE_AUX;
            newProduct.Name = NAME_AUX;
            newProduct.Price = PRICE_AUX;

            await _productsRepository.UpdateAsync(newProduct);
            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(newProduct.Image, IMAGE_AUX);
            Assert.AreEqual(newProduct.Status, STATUS_AUX);
            Assert.AreEqual(newProduct.Name, NAME_AUX);
            Assert.AreEqual(newProduct.Price, PRICE_AUX);
        }

        [Test]
        private async Task DeleteProduct()
        {
            await _productsRepository.DeleteAsync(newProduct);
            await _unitOfWork.SaveChangesAsync();

            var product = await _productsRepository.GetByIdAsync(newProduct.Id);

            Assert.IsNull(product);
        }
    }
}
