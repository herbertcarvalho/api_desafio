using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    internal class CompanyRepository : RepositoryAsync<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
