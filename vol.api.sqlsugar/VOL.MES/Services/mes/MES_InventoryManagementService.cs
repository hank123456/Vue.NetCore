/*
 *Author：jxx
 *Contact：xx
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下MES_InventoryManagementService与IMES_InventoryManagementService中编写
 */
using VOL.MES.IRepositories;
using VOL.MES.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.MES.Services
{
    public partial class MES_InventoryManagementService : ServiceBase<MES_InventoryManagement, IMES_InventoryManagementRepository>
    , IMES_InventoryManagementService, IDependency
    {
    public static IMES_InventoryManagementService Instance
    {
      get { return AutofacContainerModule.GetService<IMES_InventoryManagementService>(); } }
    }
 }
