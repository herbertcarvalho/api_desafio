using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Services.Interfaces
{
    public interface IMinIoServices
    {
        Task<string> UploadFileAsync(string objectName, string base64String);
        Task DeleteFileAsync(string objectName);
        string GetDownloadLinkAsync(string objectName);
    }
}
