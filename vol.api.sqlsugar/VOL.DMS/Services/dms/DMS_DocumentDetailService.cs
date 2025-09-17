/*
 *Author：jxx
 *Contact：xx
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下DMS_DocumentDetailService与IDMS_DocumentDetailService中编写
 */
using VOL.DMS.IRepositories;
using VOL.DMS.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Services
{
    public partial class DMS_DocumentDetailService : ServiceBase<DMS_DocumentDetail, IDMS_DocumentDetailRepository>
    , IDMS_DocumentDetailService, IDependency
    {
    public static IDMS_DocumentDetailService Instance
    {
      get { return AutofacContainerModule.GetService<IDMS_DocumentDetailService>(); } }
    }
 }
