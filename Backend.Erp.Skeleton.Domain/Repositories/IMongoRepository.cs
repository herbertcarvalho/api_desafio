using Backend.Erp.Skeleton.Domain.Auxiliares;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task InsertOneAsync(string collection, TDocument document);
    }
}
