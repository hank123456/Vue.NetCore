/*
 *Author：jxx
 *Contact：xx
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下DMS_FileStorageService与IDMS_FileStorageService中编写
 */
using VOL.DMS.IRepositories;
using VOL.DMS.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Services
{
    public partial class DMS_FileStorageService : ServiceBase<DMS_FileStorage, IDMS_FileStorageRepository>
    , IDMS_FileStorageService, IDependency
    {
    public static IDMS_FileStorageService Instance
    {
      get { return AutofacContainerModule.GetService<IDMS_FileStorageService>(); } }
    }
 }
