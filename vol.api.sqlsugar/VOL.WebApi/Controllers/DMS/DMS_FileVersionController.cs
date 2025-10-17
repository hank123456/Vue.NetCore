/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹DMS_FileVersionController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DMS.IServices;
namespace VOL.DMS.Controllers
{
    [Route("api/DMS_FileVersion")]
    [PermissionTable(Name = "DMS_FileVersion")]
    public partial class DMS_FileVersionController : ApiBaseController<IDMS_FileVersionService>
    {
        public DMS_FileVersionController(IDMS_FileVersionService service)
        : base(service)
        {
        }
    }
}

