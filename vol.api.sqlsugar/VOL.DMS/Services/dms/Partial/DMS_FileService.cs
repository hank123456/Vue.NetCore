/*
 *所有关于DMS_File类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*DMS_FileService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Utilities;
using VOL.DMS.IRepositories;
using VOL.Entity.DomainModels;
using VOL.Core.ManageUser;

namespace VOL.DMS.Services
{
    public partial class DMS_FileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDMS_FileRepository _repository;//访问数据库
        public string fileHash = string.Empty;

        [ActivatorUtilitiesConstructor]
        public DMS_FileService(
            IDMS_FileRepository dbRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        public override PageGridData<DMS_File> GetPageData(PageDataOptions pageData)
        {
            QueryRelativeExpression = (ISugarQueryable<DMS_File> queryable) =>
            {
                // 超级管理员返回所有数据  
                if (UserContext.Current.IsSuperAdmin)
                {
                    return queryable;
                }

                // 普通用户只返回自己创建的数据  
                return queryable.Where(x => x.CreateID == UserContext.Current.UserId);
            };

            return base.GetPageData(pageData);
        }

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            return base.Add(saveDataModel);
        }

        public override WebResponseContent Upload(List<IFormFile> files)
        {
            // 首先检查所有文件的hash值，避免无效上传
            var fileInfos = new List<(IFormFile file, string hash, Guid fileGroupId)>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    continue; // 跳过空文件
                }

                // 计算文件hash
                fileHash = FileHashHelper.CalculateFileHash(file);

                // 检查hash值是否已存在
                var existingFile = _repository.Find(x => x.Hash == fileHash && (x.Enable == 1)).FirstOrDefault();
                if (existingFile != null)
                {
                    return new WebResponseContent().Error($"文件 '{file.FileName}' 已存在重复的文件(Hash: {fileHash})，请检查是否为重复上传");
                }
            }
            // 如果所有文件都通过hash检查，则进行上传
            var validFiles = files.Where(f => f != null && f.Length > 0).ToList();

            if (!validFiles.Any())
            {
                return new WebResponseContent().Error("没有有效的文件可上传");
            }

            return base.Upload(validFiles);
        }
    }
}
