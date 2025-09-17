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
        /// ��ȡ�ļ�����URL������ͷ���ͼƬ��ʾ��
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns></returns>
        [HttpGet("url")]
        public IActionResult GetFileUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("�ļ�·������Ϊ��");
            }

            try
            {
                var url = _fileStorageService.GetFileUrl(filePath);
                if (string.IsNullOrEmpty(url))
                {
                    return NotFound("�ļ�������");
                }

                return Ok(new { url = url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"��ȡ�ļ�URLʧ��: {ex.Message}");
            }
        }

        /// <summary>
        /// �����ļ���������ֱ����ʾͼƬ��
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns></returns>
        [HttpGet("stream")]
        public async Task<IActionResult> GetFileStream(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("�ļ�·������Ϊ��");
            }

            try
            {
                // ������Ҫ��MinioFileStorageService����ӻ�ȡ�ļ����ķ���
                var url = _fileStorageService.GetFileUrl(filePath);
                if (string.IsNullOrEmpty(url))
                {
                    return NotFound("�ļ�������");
                }

                // �ض���MinioԤǩ��URL
                return Redirect(url);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"��ȡ�ļ�ʧ��: {ex.Message}");
            }
        }
    }
}