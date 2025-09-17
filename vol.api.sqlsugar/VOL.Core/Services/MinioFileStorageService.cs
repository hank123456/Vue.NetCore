using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using VOL.Core.Configuration;
using VOL.Core.Utilities;
using VOL.Entity;

namespace VOL.Core.Services
{
    /// <summary>
    /// Minio�ļ��洢����
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
                return response.Error("��ѡ��Ҫ�ϴ����ļ�");
            }

            try
            {
                // ȷ���洢bucket����
                var bucketExistsArgs = new BucketExistsArgs().WithBucket(_minioConfig.BucketName);
                bool bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs);
                if (!bucketExists)
                {
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(_minioConfig.BucketName);
                    await _minioClient.MakeBucketAsync(makeBucketArgs);
                }

                // ������������
                string objectName = !string.IsNullOrEmpty(folder)
                    ? $"{folder}/{DateTime.Now:yyyyMMdd}/{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{file.FileName}"
                    : $"{DateTime.Now:yyyyMMdd}/{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{file.FileName}";

                // �ϴ��ļ�
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

                return response.OK("�ļ��ϴ��ɹ�", objectName);
            }
            catch (Exception ex)
            {
                return response.Error($"�ļ��ϴ�ʧ�ܣ�{ex.Message}");
            }
        }

        /// <summary>
        /// �����ϴ��ļ���ͬ������������ServiceBase��
        /// </summary>
        public WebResponseContent UploadFiles(List<IFormFile> files, string folder = null)
        {
            if (files == null || !files.Any())
            {
                return new WebResponseContent().Error("��ѡ��Ҫ�ϴ����ļ�");
            }

            var response = new WebResponseContent(true);
            var uploadResults = new List<object>();

            try
            {
                // ȷ���洢bucket����
                var bucketExistsArgs = new BucketExistsArgs().WithBucket(_minioConfig.BucketName);
                bool bucketExists = Task.Run(async () => await _minioClient.BucketExistsAsync(bucketExistsArgs)).Result;

                if (!bucketExists)
                {
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(_minioConfig.BucketName);
                    Task.Run(async () => await _minioClient.MakeBucketAsync(makeBucketArgs)).Wait();

                }


                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        continue; // �������ļ�
                    }

                    // ������������
                    string objectName = !string.IsNullOrEmpty(folder)
                        ? $"{folder}/{DateTime.Now:yyyyMMdd}/{file.FileName}"
                        : $"{DateTime.Now:yyyyMMdd}/{file.FileName}";

                    // �ϴ��ļ�
                    using (var stream = file.OpenReadStream())
                    {
                        var putObjectArgs = new PutObjectArgs()
                            .WithBucket(_minioConfig.BucketName)
                            .WithObject(objectName)
                            .WithStreamData(stream)
                            .WithObjectSize(file.Length)
                            .WithContentType(file.ContentType);

                        Task.Run(async () => await _minioClient.PutObjectAsync(putObjectArgs)).Wait();

                    }

                    // �����ļ���ϣ
                    string fileHash = FileHashHelper.CalculateFileHash(file);

                    uploadResults.Add(new
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        ContentType = file.ContentType,
                        MinioBucket = _minioConfig.BucketName,
                        MinioObject = objectName,
                        Hash = fileHash,
                        Url = GetFileDownloadUrl(objectName)
                    });
                }

                return response.OK("�ļ��ϴ��ɹ�", uploadResults);
            }
            catch (Exception ex)
            {
                return response.Error($"�ļ��ϴ�ʧ�ܣ�{ex.Message}");
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

        /// <summary>
        /// ��ȡ����URL����ǩ����1Сʱ��Ч�ڣ�
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string GetFileDownloadUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            try
            {
                // ����ļ��Ƿ���ڣ�������ʱ�׳��쳣�����ؿ��ַ���
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(filePath);
                _minioClient.StatObjectAsync(statObjectArgs).GetAwaiter().GetResult();


                var args = new PresignedGetObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(filePath)
                    .WithExpiry(60 * 60 * 1)
                    .WithHeaders(new Dictionary<string, string>
                    {
                        // ������ǿ������������ļ��������Ǵ�
                        ["response-content-disposition"] = "attachment"
                    }); // 

                return _minioClient.PresignedGetObjectAsync(args).GetAwaiter().GetResult();
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetFileUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            try
            {
                // ����ļ��Ƿ����
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(filePath);
                _minioClient.StatObjectAsync(statObjectArgs).GetAwaiter().GetResult();

                var args = new PresignedGetObjectArgs()
                    .WithBucket(_minioConfig.BucketName)
                    .WithObject(filePath)
                    .WithExpiry(60 * 60 * 4); // 4Сʱ��Ч�ڣ��ʺ�ͷ����ʾ

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