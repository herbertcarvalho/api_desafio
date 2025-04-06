using Backend.Erp.Skeleton.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Tests.IntegrationTest;
using static Tests.TestUtils;

namespace Tests
{
    public class IntegrationDataTestStarter
    {
        private readonly IServiceProvider _serviceProvider;

        public IntegrationDataTestStarter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region services
        protected IUnitOfWork UnitOfWork => _serviceProvider.GetRequiredService<IUnitOfWork>();

        protected ICompanyRepository CompanyRepository =>
            _serviceProvider.GetRequiredService<ICompanyRepository>();

        protected IPersonsRepository PersonsRepository => _serviceProvider.GetRequiredService<IPersonsRepository>();

        protected ICategoriesRepository CategoriesRepository => _serviceProvider.GetRequiredService<ICategoriesRepository>();
        #endregion

        public async Task ExecuteSeedDataTests(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("Starting Integration Tests");
            await new CompanyIntegrationDataTest(CompanyRepository, UnitOfWork).ExecuteTestsAsync(report);
            await new PersonsIntegrationDataTest(PersonsRepository, UnitOfWork).ExecuteTestsAsync(report);
            await new CategoriesIntegrationDataTest(CategoriesRepository, UnitOfWork).ExecuteTestsAsync(report);
        }
    }
}
