using Backend.Erp.Skeleton.Domain.Entities;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface IProductsRepository : IRepositoryAsync<Products>
    {
        Task<bool> Any(int idCategory);
    }
}
