/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹DMS_DocumentFileRepository编写代码
 */
using VOL.DMS.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Repositories
{
    public partial class DMS_DocumentFileRepository : RepositoryBase<DMS_DocumentFile>
    , IDMS_DocumentFileRepository
    {
    public DMS_DocumentFileRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static IDMS_DocumentFileRepository Instance
    {
    get {  return AutofacContainerModule.GetService<IDMS_DocumentFileRepository>
        (); } }
        }
        }
