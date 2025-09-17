/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹DMS_DocumentDetailRepository编写代码
 */
using VOL.DMS.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Repositories
{
    public partial class DMS_DocumentDetailRepository : RepositoryBase<DMS_DocumentDetail>
    , IDMS_DocumentDetailRepository
    {
    public DMS_DocumentDetailRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static IDMS_DocumentDetailRepository Instance
    {
    get {  return AutofacContainerModule.GetService<IDMS_DocumentDetailRepository>
        (); } }
        }
        }
