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
        /// �����ļ����� - ֧�ִ�ͳ��URL���ʷ�ʽ
        /// �÷���/api/FileProxy/image?path=/Upload/xxxx.jpg �� /api/FileProxy/image/Upload/xxxx.jpg
        /// </summary>
        /// <param name="path">�ļ�·��</param>
        /// <returns></returns>
        [HttpGet("image")]
        [AllowAnonymous]
        public IActionResult GetImage([FromQuery] string path)
        {
            return GetImageByPath(path);
        }

        /// <summary>
        /// ֧��·�������ķ�ʽ�����ļ�
        /// �÷���/api/FileProxy/image/Upload/Tables/Sys_User/xxx.jpg
        /// </summary>
        /// <param name="path">�ļ�·����֧��б�ָܷ���</param>
        /// <returns></returns>
        [HttpGet("image/{*path}")]
        [AllowAnonymous]
        public IActionResult GetImageByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return BadRequest("�ļ�·������Ϊ��");
            }

            try
            {
                // ����·�����Ƴ�ǰ��б��
                path = path.TrimStart('/');

                // �����Minio�洢����ȡԤǩ��URL���ض���
                var storageType = AppSetting.Configuration.GetSection("FileStorage:Type").Value ?? "Local";

                if (storageType.Equals("Minio", StringComparison.OrdinalIgnoreCase))
                {
                    // ʹ��Minio�洢�����ȡ�ļ�URL
                    var url = _fileStorageService.GetFileUrl(path);
                    if (string.IsNullOrEmpty(url))
                    {
                        return NotFound("�ļ�������");
                    }

                    // �ض���MinioԤǩ��URL
                    return Redirect(url);
                }
                else
                {
                    // ���ش洢��ֱ�ӷ��ر����ļ�
                    var localPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), path);
                    if (!System.IO.File.Exists(localPath))
                    {
                        return NotFound("�ļ�������");
                    }

                    var fileStream = System.IO.File.OpenRead(localPath);
                    var contentType = GetContentType(path);
                    return File(fileStream, contentType);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"��ȡ�ļ�ʧ��: {ex.Message}");
            }
        }

        /// <summary>
        /// �����ļ���չ����ȡContent-Type
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