using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Services;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Configuration;
using System;
using System.IO;

namespace VOL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileProxyController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;

        public FileProxyController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// 代理文件访问 - 支持传统的URL访问方式
        /// 用法：/api/FileProxy/image?path=/Upload/xxxx.jpg 或 /api/FileProxy/image/Upload/xxxx.jpg
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        [HttpGet("image")]
        [AllowAnonymous]
        public IActionResult GetImage([FromQuery] string path)
        {
            return GetImageByPath(path);
        }

        /// <summary>
        /// 支持路径参数的方式访问文件
        /// 用法：/api/FileProxy/image/Upload/Tables/Sys_User/xxx.jpg
        /// </summary>
        /// <param name="path">文件路径（支持斜杠分隔）</param>
        /// <returns></returns>
        [HttpGet("image/{*path}")]
        [AllowAnonymous]
        public IActionResult GetImageByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return BadRequest("文件路径不能为空");
            }

            try
            {
                // 处理路径，移除前导斜杠
                path = path.TrimStart('/');

                // 如果是Minio存储，获取预签名URL并重定向
                var storageType = AppSetting.Configuration.GetSection("FileStorage:Type").Value ?? "Local";

                if (storageType.Equals("Minio", StringComparison.OrdinalIgnoreCase))
                {
                    // 使用Minio存储服务获取文件URL
                    var url = _fileStorageService.GetFileUrl(path);
                    if (string.IsNullOrEmpty(url))
                    {
                        return NotFound("文件不存在");
                    }

                    // 重定向到Minio预签名URL
                    return Redirect(url);
                }
                else
                {
                    // 本地存储，直接返回本地文件
                    var localPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), path);
                    if (!System.IO.File.Exists(localPath))
                    {
                        return NotFound("文件不存在");
                    }

                    var fileStream = System.IO.File.OpenRead(localPath);
                    var contentType = GetContentType(path);
                    return File(fileStream, contentType);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"获取文件失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 根据文件扩展名获取Content-Type
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetContentType(string path)
        {
            var extension = System.IO.Path.GetExtension(path)?.ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".svg" => "image/svg+xml",
                ".ico" => "image/x-icon",
                _ => "application/octet-stream"
            };
        }
    }
}