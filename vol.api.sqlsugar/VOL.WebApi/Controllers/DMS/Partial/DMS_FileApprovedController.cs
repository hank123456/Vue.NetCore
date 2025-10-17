/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("DMS_FileApproved",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VOL.Core.Enums;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Filters;
using VOL.Core.Middleware;
using VOL.Core.Services;
using VOL.DMS.IServices;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Controllers
{
    public partial class DMS_FileApprovedController
    {
        private readonly IDMS_FileApprovedService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public DMS_FileApprovedController(
            IDMS_FileApprovedService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="bucket">存储桶名称</param>
        /// <param name="object">对象名称</param>
        /// <param name="originalFileName">原始文件名</param>
        /// <returns>文件流</returns>
        [ActionLog("下载文件")]
        [ApiActionPermission(ActionPermissionOptions.Export)]
        [HttpPost, Route("download")]
        public ActionResult Download([FromBody] DMS_FileApproved file)
        {
            var bucket = file?.MinioBucket;
            var @object = file?.MinioObject;
            try
            {
                // 参数验证
                if (string.IsNullOrEmpty(bucket) || string.IsNullOrEmpty(@object))
                {
                    return BadRequest("存储桶名称和对象名称不能为空");
                }

                // 获取文件存储服务
                var fileStorageService = AutofacContainerModule.GetService<IFileStorageService>();
                if (fileStorageService == null)
                {
                    return StatusCode(500, "文件存储服务未初始化");
                }

                // 检查文件是否存在
                //bool fileExists = fileStorageService.ExistsAsync(@object);
                //if (!fileExists)
                //{
                //    return NotFound("文件不存在");
                //}

                // 返回下载URL
                string downloadUrl = fileStorageService.GetFileDownloadUrl(@object);
                if (!string.IsNullOrEmpty(downloadUrl))
                {
                    return Json(new
                    {
                        Status = true,
                        Message = "获取下载链接成功",
                        Data = new
                        {
                            Url = downloadUrl,
                            FileName = file?.FileName ?? Path.GetFileName(@object),
                            FileSize = file?.FileSize ?? 0,
                            ContentType =  file?.ContentType ?? "application/octet-stream",
                            MinioBucket = bucket,
                            MinioObject = @object
                        }
                    });
                }

                // 无法获取预签名URL
                return Json(new
                {
                    Status = false,
                    Message = "无法生成下载链接"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = false,
                    Message = $"获取下载链接失败: {ex.Message}"
                });
            }
        }
    }
}
