using Backend.Erp.Skeleton.Domain.Entities;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface IPersonsRepository : IRepositoryAsync<Persons>
    {
        /// <summary>
        /// Este método retorna se existe algum usuário com o cpf fornecido
        /// </summary>
        /// <param name="cpf">Cpf do usuário a ser buscado</param>
        /// <returns>O resultado é um boleano</returns>
        Task<bool> Any(string cpf);
    }
}
