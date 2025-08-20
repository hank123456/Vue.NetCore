using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using System;
using System.IO;
using System.Threading.Tasks;
using VOL.Core.Configuration;
using VOL.Core.Utilities;
using VOL.Entity;

namespace VOL.Core.Services
{
    /// <summary>
    /// Minio文件存储服务
    /// </summary>
    public class MinioFileStorageService : IFileStorageService
    {
        private readonly IMinioClient _minioClient;
        private readonly MinioStorage _minioConfig;

        public MinioFileStorageService(IOptions<FileStorage> fileStorageOptions)
        {
            var fileStorageConfig = fileStorageOptions.Value;
            _minioConfig = fileStorageConfig.Minio;

            _minioClient = new MinioClient()
                .WithEndpoint(_minioConfig.Endpoint)
                .WithCredentials(_minioConfig.AccessKey, _minioConfig.SecretKey)
                .WithSSL(_minioConfig.Secure)
                .WithRegion(_minioConfig.Region)
                .Build();
        }

        public async Task<WebResponseContent> UploadAsync(IFormFile file, string folder = null)
        {
            var response = new WebResponseContent(true);

            if (file == null || file.Length == 0)
            {
                return response.Error("请选择要上传的文件");
            }

            try
            {
                // 确保存储bucket存在
                var bucketExistsArgs = new BucketExistsArgs().WithBucket(_minioConfig.BucketName);
                bool bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs);
                if (!bucketExists)
                {
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(_minioConfig.BucketName);
                    await _minioClient.MakeBucketAsync(makeBucketArgs);
                }

                // 构建对象名称
                string objectName = !string.IsNullOrEmpty(folder)
                    ? $"{folder}/{DateTime.Now:yyyyMMdd}/{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{file.FileName}"
                    : $"{DateTime.Now:yyyyMMdd}/{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{file.FileName}";

                // 上传文件
                using (var stream = file.OpenReadStream())
                {
                    var putObjectArgs = new PutObjectArgs()
                        .WithBucket(_minioConfig.BucketName)
                        .WithObject(objectName)
                        .WithStreamData(stream)
                        .WithObjectSize(file.Length)
                        .WithContentType(file.ContentType);

                    await _minioClient.PutObjectAsync(putObjectArgs);
                }

                return response.OK("文件上传成功", objectName);
            }
            catch (Exception ex)
            {
                return response.Error($"文件上传失败：{ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(string filePath)
        {
            try
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(filePath);

                await _minioClient.RemoveObjectAsync(removeObjectArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetFileUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            try
            {
                // 生成预签名URL，有效期24小时
                var args = new PresignedGetObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(filePath)
                    .WithExpiry(60 * 60 * 24); // 24小时

                return _minioClient.PresignedGetObjectAsync(args).GetAwaiter().GetResult();
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<bool> ExistsAsync(string filePath)
        {
            try
            {
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(filePath);

                await _minioClient.StatObjectAsync(statObjectArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}