using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    internal class ProductsRepository : RepositoryAsync<Products>, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Any(int idCategory)
            => await Query().AnyAsync(x => x.IdCategory == idCategory);

        public async Task<PaginatedResult<Products>> GetFiltered(int? idCompany, int? idCategory, decimal? minPrice, decimal? maxPrice, PageOption pageOption, bool active = true)
        {
            var query = Query()
                .Include(x => x.Category)
                .Include(x => x.Company)
                .Where(x => x.Status == active);

            if (idCompany.HasValue)
                query = query.Where(x => x.IdCompany == idCompany);

            if (idCategory.HasValue)
                query = query.Where(x => x.IdCategory == idCategory);

            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice);

            return await query.ToPaginatedList(pageOption.Page, pageOption.PageSize);
        }
    }
}
