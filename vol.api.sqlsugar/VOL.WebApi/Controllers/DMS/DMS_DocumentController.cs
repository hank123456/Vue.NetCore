/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹DMS_DocumentController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DMS.IServices;
namespace VOL.DMS.Controllers
{
    [Route("api/DMS_Document")]
    [PermissionTable(Name = "DMS_Document")]
    public partial class DMS_DocumentController : ApiBaseController<IDMS_DocumentService>
    {
        public DMS_DocumentController(IDMS_DocumentService service)
        : base(service)
        {
        }
    }
}

