using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using VOL.Core.Configuration;
using VOL.Core.Extensions;
using VOL.Core.Utilities;
using VOL.Entity;

namespace VOL.Core.Services
{
    /// <summary>
    /// �����ļ��洢����
    /// </summary>
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly FileStorage _fileStorageConfig;

        public LocalFileStorageService(IOptions<FileStorage> fileStorageOptions)
        {
            _fileStorageConfig = fileStorageOptions.Value;
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
                // �����ļ��洢·��
                string uploadPath = !string.IsNullOrEmpty(folder) 
                    ? $"{_fileStorageConfig.Local.UploadPath}/{folder}/" 
                    : $"{_fileStorageConfig.Local.UploadPath}/{DateTime.Now:yyyyMMdd}/";

                string fullPath = uploadPath.MapPath(true);

                // ȷ��Ŀ¼����
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                // �����ļ���������ԭ�ļ������������������UUIDǰ׺����������
                string fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{file.FileName}";
                string filePath = Path.Combine(fullPath, fileName);

                // �����ļ�
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // �������·��
                string relativePath = $"{uploadPath}{fileName}";
                return response.OK("�ļ��ϴ��ɹ�", relativePath);
            }
            catch (Exception ex)
            {
                return response.Error($"�ļ��ϴ�ʧ�ܣ�{ex.Message}");
            }
        }

        //public async Task<bool> DeleteAsync(string filePath)
        //{
        //    try
        //    {
        //        string fullPath = filePath.MapPath();
        //        if (File.Exists(fullPath))
        //        {
        //            File.Delete(fullPath);
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}  //ͬ�����������һЩ
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
                // �����ļ��洢·��
                string uploadPath = !string.IsNullOrEmpty(folder)
                    ? $"{_fileStorageConfig.Local.UploadPath}/{folder}/"
                    : $"{_fileStorageConfig.Local.UploadPath}/{DateTime.Now:yyyyMMdd}/";

                string fullPath = uploadPath.MapPath(true);

                // ȷ��Ŀ¼����
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        continue; // �������ļ�
                    }

                    // �����ļ���
                    string fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{file.FileName}";
                    string filePath = Path.Combine(fullPath, fileName);

                    // �����ļ�
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // �����ļ���ϣ
                    string fileHash = CalculateFileHash(file);

                    // �������·��
                    string relativePath = $"{uploadPath}{fileName}";

                    uploadResults.Add(new
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        ContentType = file.ContentType,
                        MinioObject = relativePath, // ���ش洢ʹ�����·��
                        Hash = fileHash,
                        Url = GetFileDownloadUrl(relativePath)
                    });
                }

                return response.OK("�ļ��ϴ��ɹ�", uploadResults);
            }
            catch (Exception ex)
            {
                return response.Error($"�ļ��ϴ�ʧ�ܣ�{ex.Message}");
            }
        }

        /// <summary>
        /// �����ļ���ϣֵ
        /// </summary>
        private string CalculateFileHash(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return Convert.ToBase64String(hash);
            }
        }

        public Task<bool> DeleteAsync(string filePath)
        {
            try
            {
                string fullPath = filePath.MapPath();
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public string GetFileDownloadUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            // ���ش洢ֱ�ӷ�������·��
            return filePath.StartsWith(_fileStorageConfig.Local.VirtualPath) 
                ? filePath 
                : $"{_fileStorageConfig.Local.VirtualPath}/{filePath.TrimStart('/')}";
        }

        //public async Task<bool> ExistsAsync(string filePath)
        //{
        //    try
        //    {
        //        string fullPath = filePath.MapPath();
        //        return File.Exists(fullPath);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        public Task<bool> ExistsAsync(string filePath)
        {
            try
            {
                string fullPath = filePath.MapPath();
                return Task.FromResult(File.Exists(fullPath));
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public string GetFileUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // ���ش洢ֱ�ӷ�������·��
            return path.StartsWith(_fileStorageConfig.Local.VirtualPath)
                ? path
                : $"{_fileStorageConfig.Local.VirtualPath}/{path.TrimStart('/')}";
        }
    }
}