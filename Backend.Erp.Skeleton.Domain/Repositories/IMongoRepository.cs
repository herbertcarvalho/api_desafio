using Backend.Erp.Skeleton.Domain.Helper;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Repositories
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task InsertOneAsync(string collection, TDocument document);
    }
}
