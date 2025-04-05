using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface ICategoriesRepository : IRepositoryAsync<Categories>
    {
        /// <summary>
        /// Este método retorna se existe alguma categoria com o nome fornecido
        /// </summary>
        /// <param name="name">Nome da categoria a ser buscada</param>
        /// <returns>O resultado é um boleano</returns>
        Task<bool> Any(string name);

        /// <summary>
        /// Este método retorna uma lista de categorias de forma paginada
        /// </summary>
        /// <param name="name">Nome da categoria a ser buscada</param>
        /// <param name="pageOption">Estrutura utilizada para selecionar as paginas</param>
        /// <returns>O resultado é uma lista de categorias filtrada com os campos</returns>
        Task<PaginatedResult<Categories>> GetFiltered(string name, PageOption pageOption);

        /// <summary>
        /// Este método retorna se existe alguma categoria com o nome fornecido
        /// </summary>
        /// <param name="id">Id categoria a ser buscada</param>
        /// <returns>O resultado é um boleano</returns>
        Task<bool> Any(int id);
    }
}
