/*
 *所有关于DMS_DocumentDetail类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*DMS_DocumentDetailService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Minio.DataModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using VOL.Core.BaseProvider;
using VOL.Core.Configuration;
using VOL.Core.Extensions;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Services;
using VOL.Core.Utilities;
using VOL.DMS.IRepositories;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Services
{
    public partial class DMS_DocumentDetailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDMS_DocumentDetailRepository _repository;//访问数据库

        private readonly IFileStorageService _fileStorageService;

        [ActivatorUtilitiesConstructor]
        public DMS_DocumentDetailService(
            IDMS_DocumentDetailRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService fileStorageService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _fileStorageService = fileStorageService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        /// <summary>
        /// 重写上传方法，保存到Minio并记录到数据库
        /// </summary>
        public override WebResponseContent Upload(List<IFormFile> files)
        {
            return base.Upload(files);

        }
        /// <summary>
        /// 获取文件下载URL
        /// </summary>
        public WebResponseContent GetFileUrl(Guid documentId)
        {
            var document = repository.Find(x => x.Id == documentId).FirstOrDefault();
            if (document == null)
            {
                return new WebResponseContent().Error("文件不存在");
            }

            var url = _fileStorageService.GetFileDownloadUrl(document.MinioObject);
            return new WebResponseContent().OK("获取成功", new { Url = url });
        }
  }
}
