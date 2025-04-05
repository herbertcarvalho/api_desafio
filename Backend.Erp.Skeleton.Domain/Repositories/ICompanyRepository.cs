using Backend.Erp.Skeleton.Domain.Entities;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface ICompanyRepository : IRepositoryAsync<Company>
    {
        /// <summary>
        /// Este método retorna se existe alguma empresa com cnpj fornecido
        /// </summary>
        /// <param name="cnpj">Cnpj da empresa a ser buscada</param>
        /// <returns>O resultado é um boleano</returns>
        Task<bool> Any(string cnpj);

        /// <summary>
        /// Este método retorna se existe alguma empresa com o id fornecido
        /// </summary>
        /// <param name="id">Id da empresa a ser buscada</param>
        /// <returns>O resultado é um boleano</returns>
        Task<bool> Any(int id);
    }
}
