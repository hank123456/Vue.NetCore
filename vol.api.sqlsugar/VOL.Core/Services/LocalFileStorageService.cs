using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
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

        public string GetFileUrl(string filePath)
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
    }
}