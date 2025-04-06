using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using NUnit.Framework;
using Tests.Interfaces;
using static Tests.TestUtils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.IntegrationTest
{
    public class CategoriesIntegrationDataTest(
        ICategoriesRepository categoriesRepository,
        IUnitOfWork unitOfWork) : ITest
    {
        private readonly ICategoriesRepository _categoriesRepository = categoriesRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("CategoriesIntegrationDataTest");
            await MapCategory();
            await ExecuteAssert(ExecutePostCategory(), report, "ExecutePostCategory");
            await ExecuteAssert(AnyName(), report, "AnyName");
            await ExecuteAssert(AnyId(), report, "AnyId");
            await ExecuteAssert(GetFiltered(), report, "GetFiltered");
            await ExecuteAssert(UpdateCategory(), report, "UpdateCategory");
            await ExecuteAssert(DeleteCategory(), report, "DeleteCategory");
        }

        private Categories newCategory = new();
        private const string CATEGORY_NAME = "Category new name";

        private const string CATEGORY_NAME_AUX = "Category new name XXX";

        private async Task MapCategory()
        {
            await _categoriesRepository.AddAsync(new Categories()
            {
                Name = "Category mapping",
                CreatedAt = DateTime.UtcNow,
                IdCreatedBy = 1
            });

            await _unitOfWork.SaveChangesAsync();
        }

        [Test]
        private async Task ExecutePostCategory()
        {
            var category = await _categoriesRepository.AddAsync(new Categories()
            {
                Name = CATEGORY_NAME,
                CreatedAt = DateTime.UtcNow,
                IdCreatedBy = 1
            });

            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(category.Name, CATEGORY_NAME);

            newCategory = category;
        }

        [Test]
        private async Task AnyName()
        {
            var isCategoryFound = await _categoriesRepository.Any(CATEGORY_NAME);

            Assert.AreEqual(isCategoryFound, true);
        }

        [Test]
        private async Task AnyId()
        {
            var isCategoryFound = await _categoriesRepository.Any(newCategory.Id);

            Assert.AreEqual(isCategoryFound, true);
        }

        [Test]
        private async Task GetFiltered()
        {
            var categories = await _categoriesRepository.GetFiltered(null, new PageOption()
            {
                Page = 1,
                PageSize = 100
            });

            var selectedCategory = categories.Data.Last();
            if (selectedCategory is null)
                Assert.Fail("Não deveria ser null.");

            Assert.AreEqual(selectedCategory.Name, CATEGORY_NAME);
        }

        [Test]
        private async Task UpdateCategory()
        {
            newCategory.Name = CATEGORY_NAME_AUX;

            await _categoriesRepository.UpdateAsync(newCategory);
            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(newCategory.Name, CATEGORY_NAME_AUX);
        }

        [Test]
        private async Task DeleteCategory()
        {
            await _categoriesRepository.DeleteAsync(newCategory);
            await _unitOfWork.SaveChangesAsync();

            var category = await _categoriesRepository.GetByIdAsync(newCategory.Id);

            Assert.IsNull(category);
        }
    }
}
