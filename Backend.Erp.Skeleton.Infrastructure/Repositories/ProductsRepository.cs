using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    internal class ProductsRepository : RepositoryAsync<Products>, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
