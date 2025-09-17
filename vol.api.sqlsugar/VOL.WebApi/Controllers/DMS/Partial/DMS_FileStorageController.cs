/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("DMS_FileStorage",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VOL.Core.Enums;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Filters;
using VOL.Core.ManageUser;
using VOL.Core.Middleware;
using VOL.Core.Services;
using VOL.DMS.IRepositories;
using VOL.DMS.IServices;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Controllers
{
    public partial class DMS_FileStorageController
    {
        private readonly IDMS_FileStorageService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDMS_FileStorageRepository _fileStorageRepository;

        [ActivatorUtilitiesConstructor]
        public DMS_FileStorageController(
            IDMS_FileStorageService service,
            IHttpContextAccessor httpContextAccessor,
            IDMS_FileStorageRepository fileStorageRepository
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _fileStorageRepository = fileStorageRepository;
        }

        /// <summary>
        /// 获取最新版本的文件分组数据
        /// </summary>
        /// <param name="pageData">分页查询参数</param>
        /// <returns>返回IsCurrentVersion=1的文件数据</returns
        [ActionLog("查询文件最新版本")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        [HttpPost, Route("CurrentVersion/getPageData")]
        public ActionResult GetPageDataCurrentVersion([FromBody] PageDataOptions pageData)
        {
            var conditions = new List<SearchParameters>
            {
                new SearchParameters { Name = "IsCurrentVersion", Value = "1", DisplayType = "=" },
                new SearchParameters { Name = "Enable", Value = "1", DisplayType = "=" }
            };
            if (pageData.Filter != null && pageData.Filter.Count > 0)
            {
                conditions.AddRange(pageData.Filter.Where(x => x.Name != "IsCurrentVersion" && x.Name != "Enable"));
            }
            // 如果有Wheres条件，也合并进去
            else if (!string.IsNullOrEmpty(pageData.Wheres))
            {
                try
                {
                    var wheresConditions = JsonConvert.DeserializeObject<List<SearchParameters>>(pageData.Wheres);
                    if (wheresConditions != null)
                    {
                        conditions.AddRange(wheresConditions.Where(x => x.Name != "IsCurrentVersion" && x.Name != "Enable"));
                    }
                }
                catch { } // 忽略反序列化错误
            }

            // 重新设置查询条件
            pageData.Wheres = JsonConvert.SerializeObject(conditions);
            pageData.Filter = null; // 清空Filter，让基类处理Wheres

            return base.GetPageData(pageData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageData"></param>
        /// <returns></returns>
        [ActionLog("查询文件历史版本")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        [HttpPost, Route("getHistorys")]
        public ActionResult GetPageDataHistory([FromBody] DMS_FileStorage fileStorage)
        {
            try
            {
                // 参数验证
                if (fileStorage == null || fileStorage.FileGroupId == Guid.Empty)
                {
                    return Json(new
                    {
                        Status = false,
                        Message = "FileGroupId参数不能为空"
                    });
                }

                // 直接查询数据库获取历史版本
                var historyFiles = _fileStorageRepository.Find(x =>
                    x.FileGroupId == fileStorage.FileGroupId &&
                    x.IsCurrentVersion == 0 &&
                    x.Enable == 1)
                    .OrderByDescending(x => x.CreateDate)
                    .ToList();

                // 返回标准格式的响应
                return Json(new
                {
                    Status = true,
                    Message = "查询成功",
                    Data = new
                    {
                        rows = historyFiles,
                        total = historyFiles.Count
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = false,
                    Message = $"查询历史版本失败: {ex.Message}"
                });
            }
        }


        [ActionLog("文件最新版本页面新增")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        [HttpPost, Route("CurrentVersion/Add")]
        public ActionResult AddNew([FromBody] SaveModel saveModel)
        {

            if (saveModel == null)
            {
                return BadRequest("保存模型不能为空");
            }
            
            return base.Add(saveModel);
        }


        [ActionLog("禁用文件")]
        [ApiActionPermission(ActionPermissionOptions.Update)]
        [HttpPost, Route("deletFileStorageItem")]
        public IActionResult deletFileStorageItem([FromBody] DMS_FileStorage fileStorage)
        {
            try
            {
                if(fileStorage == null || fileStorage.Id == Guid.Empty)
                {
                    return Content("参数错误, Id不能为空");
                }

                fileStorage.Enable = 0;
                fileStorage.ModifyID = UserContext.Current.UserId;
                fileStorage.Modifier = UserContext.Current.UserTrueName;
                fileStorage.ModifyDate = DateTime.Now;

                _fileStorageRepository.Update(fileStorage, x => new
                {
                    x.Enable,
                    x.ModifyID,
                    x.Modifier,
                    x.ModifyDate
                },true);

                return Content("删除操作成功");
            }catch(Exception ex)
            {
                return Content("删除操作失败, " + ex.Message);
                
            }
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
        public ActionResult Download([FromBody] DMS_FileStorage fileStorage)
        {
            var bucket = fileStorage?.MinioBucket;
            var @object = fileStorage?.MinioObject;
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
                            FileName = fileStorage?.FileName ?? Path.GetFileName(@object),
                            FileSize = fileStorage?.FileSize ?? 0,
                            ContentType = fileStorage?.ContentType ?? "application/octet-stream",
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
