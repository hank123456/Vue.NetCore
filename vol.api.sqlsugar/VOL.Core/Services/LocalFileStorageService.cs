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
    /// 本地文件存储服务
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
                return response.Error("请选择要上传的文件");
            }

            try
            {
                // 构建文件存储路径
                string uploadPath = !string.IsNullOrEmpty(folder) 
                    ? $"{_fileStorageConfig.Local.UploadPath}/{folder}/" 
                    : $"{_fileStorageConfig.Local.UploadPath}/{DateTime.Now:yyyyMMdd}/";

                string fullPath = uploadPath.MapPath(true);

                // 确保目录存在
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                // 生成文件名（保持原文件名，可以在这里添加UUID前缀避免重名）
                string fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{file.FileName}";
                string filePath = Path.Combine(fullPath, fileName);

                // 保存文件
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 返回相对路径
                string relativePath = $"{uploadPath}{fileName}";
                return response.OK("文件上传成功", relativePath);
            }
            catch (Exception ex)
            {
                return response.Error($"文件上传失败：{ex.Message}");
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
        //}  //同步好像更合适一些


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

            // 本地存储直接返回虚拟路径
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