/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("DMS_FileCatalog",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOL.Core.BaseProvider;
using VOL.DMS.IRepositories;
using VOL.DMS.IServices;
using VOL.Entity.DomainModels;
using VOL.MES.IRepositories;

namespace VOL.DMS.Controllers
{
    public partial class DMS_FileCatalogController
    {
        private readonly IDMS_FileCatalogService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDMS_FileCatalogRepository _repository;//访问业务代码

        [ActivatorUtilitiesConstructor]
        public DMS_FileCatalogController(
            IDMS_FileCatalogService service,
            IHttpContextAccessor httpContextAccessor,
            IDMS_FileCatalogRepository repository
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }


        /// <summary>
        /// 文件信息tree页面获取左边的tree的所有文件分类
        /// </summary>
        /// <returns></returns>
        [Route("getList"), HttpGet]
        public async Task<IActionResult> GetList()
        {
            var data = await _repository.FindAsIQueryable(x => true)
                  .Select(s => new
                  {
                      id = s.CatalogID,
                      s.ParentId,
                      name = s.CatalogName
                  })
                  .ToListAsync();
            return Json(data);
        }
    }
}
