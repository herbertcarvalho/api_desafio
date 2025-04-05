using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    internal class CompanyRepository : RepositoryAsync<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Any(string cnpj)
            => await Query().AnyAsync(x => x.Cnpj == cnpj);

        public async Task<bool> Any(int id)
            => await Query().AnyAsync(x => x.Id == id);
    }
}
