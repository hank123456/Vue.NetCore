/*
 *Author：jxx
 *Contact：xx
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Sys_WorkFlowStepService与ISys_WorkFlowStepService中编写
 */
using VOL.Sys.IRepositories;
using VOL.Sys.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.Sys.Services
{
    public partial class Sys_WorkFlowStepService : ServiceBase<Sys_WorkFlowStep, ISys_WorkFlowStepRepository>
    , ISys_WorkFlowStepService, IDependency
    {
    public Sys_WorkFlowStepService(ISys_WorkFlowStepRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static ISys_WorkFlowStepService Instance
    {
      get { return AutofacContainerModule.GetService<ISys_WorkFlowStepService>(); } }
    }
 }
