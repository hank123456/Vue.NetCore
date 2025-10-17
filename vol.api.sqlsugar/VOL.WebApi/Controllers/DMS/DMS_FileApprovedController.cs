/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹DMS_FileApprovedController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DMS.IServices;
namespace VOL.DMS.Controllers
{
    [Route("api/DMS_FileApproved")]
    [PermissionTable(Name = "DMS_FileApproved")]
    public partial class DMS_FileApprovedController : ApiBaseController<IDMS_FileApprovedService>
    {
        public DMS_FileApprovedController(IDMS_FileApprovedService service)
        : base(service)
        {
        }
    }
}

