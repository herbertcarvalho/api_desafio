using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    internal class PersonsRepository : RepositoryAsync<Persons>, IPersonsRepository
    {
        public PersonsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Any(string cpf)
            => await Query().AnyAsync(x => x.Cpf == cpf);

        public async Task<Persons> Get(int idUser)
            => await Query().FirstOrDefaultAsync(x => x.IdUser == idUser);
    }
}
