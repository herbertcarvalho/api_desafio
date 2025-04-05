using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Services.Interfaces
{
    public interface IAws3Services
    {
        Task<string> UploadFileAsync(string file, string identifier, int? contentTypeId = null);
        Task DeleteFileAsync(string fileName);
    }
}
