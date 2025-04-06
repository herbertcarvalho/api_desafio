using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Services
{
    public class MinIoServices : IMinIoServices
    {
        private readonly IMinioClient _minioClient;
        private readonly string _bucketName;
        private readonly string _url;

        public MinIoServices(IConfiguration configuration)
        {
            var configSection = configuration.GetSection("AWS");
            _url = configSection["Url"];
            _bucketName = configSection["BucketName"];
            string login = configSection["AWSAccessKeyId"];
            string password = configSection["AWSSecretKey"];
            _minioClient = new MinioClient()
                .WithEndpoint("minio", 9000)
                .WithCredentials(login, password)
                .WithSSL(false)
                .Build();
        }

        public async Task<string> UploadFileAsync(string objectName, string base64String)
        {
            await EnsureBucketExistsAsync();

            byte[] fileBytes = Convert.FromBase64String(base64String);

            using var stream = new MemoryStream(fileBytes);

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(fileBytes.Length)
                .WithContentType(base64String.GetMimeTypeFromBase64()));

            return GetDownloadLinkAsync(objectName);
        }

        public string GetDownloadLinkAsync(string objectName)
            => $"http://{_url}:9000/{_bucketName}/{Uri.EscapeDataString(objectName)}";

        public async Task DeleteFileAsync(string objectName)
        {
            await EnsureBucketExistsAsync();

            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName));

            Console.WriteLine($"Deleted {objectName} from bucket '{_bucketName}'.");
        }

        private async Task EnsureBucketExistsAsync()
        {
            bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!found)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
                Console.WriteLine($"Bucket '{_bucketName}' created.");
            }
        }
    }
}
