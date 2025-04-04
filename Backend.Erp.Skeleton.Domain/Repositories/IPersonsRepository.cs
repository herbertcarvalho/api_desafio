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

        /// <summary>
        /// Este método um usuário com o id usuário fornecido
        /// </summary>
        /// <param name="idUser">Id do usuário a ser buscado</param>
        /// <returns>O resultado é uma entidade Persons</returns>
        Task<Persons> Get(int idUser);
    }
}
