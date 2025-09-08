/*
 *Author：jxx
 *Contact：xx
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下MES_DefectiveProductRecordService与IMES_DefectiveProductRecordService中编写
 */
using VOL.MES.IRepositories;
using VOL.MES.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.MES.Services
{
    public partial class MES_DefectiveProductRecordService : ServiceBase<MES_DefectiveProductRecord, IMES_DefectiveProductRecordRepository>
    , IMES_DefectiveProductRecordService, IDependency
    {
    public static IMES_DefectiveProductRecordService Instance
    {
      get { return AutofacContainerModule.GetService<IMES_DefectiveProductRecordService>(); } }
    }
 }
