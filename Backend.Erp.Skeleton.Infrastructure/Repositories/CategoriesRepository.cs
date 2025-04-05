using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    public class CategoriesRepository : RepositoryAsync<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> Any(string name)
            => await Query().AnyAsync(x => x.name.ToLower() == name.ToLower());
    }
}
