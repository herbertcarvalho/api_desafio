using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    public class CategoriesRepository : RepositoryAsync<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> Any(string name)
            => await Query().AnyAsync(x => x.Name.ToLower() == name.Trim().ToLower());

        public async Task<PaginatedResult<Categories>> GetFiltered(string name, PageOption pageOption)
        {
            var query = Query();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.ToLower().Contains(name.Trim().ToLower()));

            return await query.ToPaginatedList(pageOption.Page, pageOption.PageSize);
        }
    }
}
