/*
 *Author：jxx
 *Contact：xx
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下DMS_FileService与IDMS_FileService中编写
 */
using VOL.DMS.IRepositories;
using VOL.DMS.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Services
{
    public partial class DMS_FileService : ServiceBase<DMS_File, IDMS_FileRepository>
    , IDMS_FileService, IDependency
    {
    public static IDMS_FileService Instance
    {
      get { return AutofacContainerModule.GetService<IDMS_FileService>(); } }
    }
 }
