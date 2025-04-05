using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface IProductsRepository : IRepositoryAsync<Products>
    {
        /// <summary>
        /// Este método retorna se existe algum produto com o idCategoria fornecido
        /// </summary>
        /// <param name="idCategory">Id da categoria</param>
        /// <returns>O resultado é um boleano</returns>
        Task<bool> Any(int idCategory);

        /// <summary>
        /// Este método retorna uma lista de produtos de forma paginada
        /// </summary>
        /// <param name="name">Nome da categoria a ser buscada</param>
        /// <param name="pageOption">Estrutura utilizada para selecionar as paginas</param>
        /// <returns>O resultado é uma lista de produtos filtrada com os campos</returns>
        Task<PaginatedResult<Products>> GetFiltered(int? idCompany, int? idCategory, decimal? minPrice, decimal? maxPrice, PageOption pageOption, bool active = true);

        /// <summary>
        /// Este método retorna se existe alguma imagem com o guid fornecido
        /// </summary>
        /// <param img="img">Guid a ser buscado </param>
        /// <returns>O resultado é um bool</returns>
        Task<bool> AnyImage(string img);

        /// <summary>
        /// Este método retorna se existe algum produto com o nome fornecido
        /// </summary>
        /// <param name="name">Nome do produto</param>
        /// <returns>O resultado é um boleano</returns>
        Task<bool> Any(string name);
    }
}
