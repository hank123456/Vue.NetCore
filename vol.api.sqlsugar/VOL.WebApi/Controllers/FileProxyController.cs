using Microsoft.AspNetCore.Mvc;
using VOL.Core.Services;
using VOL.Core.Extensions.AutofacManager;
using System;
using System.Threading.Tasks;

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
        /// 获取文件访问URL（用于头像等图片显示）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [HttpGet("url")]
        public IActionResult GetFileUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("文件路径不能为空");
            }

            try
            {
                var url = _fileStorageService.GetFileUrl(filePath);
                if (string.IsNullOrEmpty(url))
                {
                    return NotFound("文件不存在");
                }

                return Ok(new { url = url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"获取文件URL失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 代理文件流（用于直接显示图片）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [HttpGet("stream")]
        public async Task<IActionResult> GetFileStream(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("文件路径不能为空");
            }

            try
            {
                // 这里需要在MinioFileStorageService中添加获取文件流的方法
                var url = _fileStorageService.GetFileUrl(filePath);
                if (string.IsNullOrEmpty(url))
                {
                    return NotFound("文件不存在");
                }

                // 重定向到Minio预签名URL
                return Redirect(url);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"获取文件失败: {ex.Message}");
            }
        }
    }
}