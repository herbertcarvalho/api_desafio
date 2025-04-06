using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using NUnit.Framework;
using Tests.Interfaces;
using static Tests.TestUtils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.IntegrationTest
{
    public class CompanyIntegrationDataTest(
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork) : ITest
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("CompanyIntegrationDataTest");
            await MapCompany();
            await ExecuteAssert(ExecutePostCompany(), report, "ExecutePostCompany");
            await ExecuteAssert(AnyByCnpj(), report, "AnyByCnpj");
            await ExecuteAssert(AnyById(), report, "AnyById");
            await ExecuteAssert(GetById(), report, "GetById");
            await ExecuteAssert(UpdateCompany(), report, "UpdateCompany");
            await ExecuteAssert(DeleteCompany(), report, "DeleteCompany");
        }


        private Company newCompany = new();
        private const string CNPJ = "10563862000186";
        private const string NEW_COMPANY_NAME = "Company Testing Org";

        private const string CNPJ_AUX = "73203181000127";
        private const string NEW_COMPANY_NAME_AUX = "New Company Testing Org";

        private async Task MapCompany()
        {
            var company = await _companyRepository.AddAsync(new Company()
            {
                Cnpj = "69927156000119",
                CreatedAt = DateTime.UtcNow,
                IdCreatedBy = 1,
                Name = "Company map",
            });

            await _unitOfWork.SaveChangesAsync();
        }

        [Test]
        private async Task ExecutePostCompany()
        {
            var company = await _companyRepository.AddAsync(new Company()
            {
                Cnpj = CNPJ,
                CreatedAt = DateTime.UtcNow,
                IdCreatedBy = 1,
                Name = NEW_COMPANY_NAME,
            });

            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(company.Name, NEW_COMPANY_NAME);
            Assert.AreEqual(company.Cnpj, CNPJ);

            newCompany = company;
        }

        [Test]
        private async Task AnyByCnpj()
        {
            var exits = await _companyRepository.Any(CNPJ);

            Assert.AreEqual(exits, true);
        }

        [Test]
        private async Task AnyById()
        {
            var exits = await _companyRepository.Any(newCompany.Id);

            Assert.AreEqual(exits, true);
        }

        [Test]
        private async Task GetById()
        {
            var company = await _companyRepository.GetByIdAsync(newCompany.Id);

            Assert.AreEqual(company.Name, NEW_COMPANY_NAME);
            Assert.AreEqual(company.Cnpj, CNPJ);
        }

        [Test]
        private async Task UpdateCompany()
        {
            newCompany.Name = NEW_COMPANY_NAME_AUX;
            newCompany.Cnpj = CNPJ_AUX;

            await _companyRepository.UpdateAsync(newCompany);
            await _unitOfWork.SaveChangesAsync();

            Assert.AreEqual(newCompany.Name, NEW_COMPANY_NAME_AUX);
            Assert.AreEqual(newCompany.Cnpj, CNPJ_AUX);
        }

        [Test]
        private async Task DeleteCompany()
        {
            await _companyRepository.DeleteAsync(newCompany);
            await _unitOfWork.SaveChangesAsync();

            var company = await _companyRepository.GetByIdAsync(newCompany.Id);

            Assert.IsNull(company);
        }
    }
}
