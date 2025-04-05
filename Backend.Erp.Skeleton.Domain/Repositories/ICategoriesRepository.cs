using Backend.Erp.Skeleton.Domain.Entities;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface ICategoriesRepository : IRepositoryAsync<Categories>
    {
        Task<bool> Any(string name);
    }
}
