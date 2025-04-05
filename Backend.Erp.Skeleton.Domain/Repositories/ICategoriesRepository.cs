using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface ICategoriesRepository : IRepositoryAsync<Categories>
    {
        Task<bool> Any(string name);
        Task<PaginatedResult<Categories>> GetFiltered(string name, PageOption pageOption);
    }
}
