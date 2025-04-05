using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Backend.Erp.Skeleton.Application.DTOs.AWS3;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Services
{
    public class Aws3Services : IAws3Services
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly IConfigurationSection _configurationSection;
        private readonly AwsConfigureOptions _awsConfigureOptions;

        public Aws3Services(IAmazonS3 amazonS3, IConfiguration configuration)
        {
            _amazonS3 = amazonS3;
            _configurationSection = configuration.GetSection("AWS");
            _awsConfigureOptions = new AwsConfigureOptions()
            {
                BucketName = _configurationSection["BucketName"],
                BucketPrefix = _configurationSection["BucketPrefix"],
                Region = _configurationSection["Region"]
            };
        }

        public async Task<string> UploadFileAsync(string file, string identifier, int? contentTypeId)
        {
            if (!file.IsBase64StringAndLengthValid())
                throw new ApiException("A imagem enviada não é válida.");
            try
            {
                using (var memoryStream = new MemoryStream(Convert.FromBase64String(file)))
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = memoryStream,
                        Key = _awsConfigureOptions.BucketPrefix + identifier,
                        BucketName = _awsConfigureOptions.BucketName,
                        CannedACL = S3CannedACL.PublicRead,
                        ContentType = null,
                    };
                    var fileTransferUtility = new TransferUtility(_amazonS3);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    return GenerateUrl(_awsConfigureOptions.BucketName, _awsConfigureOptions.Region, uploadRequest.Key);
                }
            }
            catch (AmazonS3Exception)
            {
                throw new Exception("Erro ao fazer upload de arquivo");
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = _awsConfigureOptions.BucketName,
                Key = _awsConfigureOptions.BucketPrefix + fileName,
            };
            try
            {
                await _amazonS3.DeleteObjectAsync(request);
            }
            catch (AmazonS3Exception)
            {
                throw new Exception($"Erro ao deletar arquivo: {fileName}");
            }
        }

        private static string GenerateUrl(string bucketName, string awsRegion, string key)
            => $"https://{bucketName}.s3.{awsRegion}.amazonaws.com/{key}";
    }
}
