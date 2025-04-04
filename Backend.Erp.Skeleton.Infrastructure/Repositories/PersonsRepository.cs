using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using Backend.Erp.Skeleton.Infrastructure.DbContexts;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    internal class PersonsRepository : RepositoryAsync<Persons>, IPersonsRepository
    {
        public PersonsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
