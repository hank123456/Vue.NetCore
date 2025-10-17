/*
 *所有关于DMS_FileApproved类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*DMS_FileApprovedService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Linq.Expressions;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Services;
using VOL.Core.Utilities;
using VOL.DMS.IRepositories;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Services
{
    public partial class DMS_FileApprovedService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDMS_FileApprovedRepository _repository;//访问数据库
        private readonly IFileStorageService _fileStorageService;

        [ActivatorUtilitiesConstructor]
        public DMS_FileApprovedService(
            IDMS_FileApprovedRepository dbRepository,
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

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            // 检查FileId，判断文件是否和已经签署的文件通过文件签约签署平台api查询到的id一致

            string fileId = saveDataModel.MainData["FileId"].ToString();
            if (!checkFileStatus(fileId))
            {
                return new WebResponseContent().Error("上传文件不是签署过的文件，请前往内网电子签约签署平台签署后再上传！");
            }

            return base.Add(saveDataModel);
        }

        private bool checkFileStatus(string FileId)
        {
            //sx todo . 需要调用电子签约平台的api查询文件状态
            return false;
        }

        /// <summary>
        /// 重写上传方法，保存到Minio并记录到数据库
        /// </summary>
        public override WebResponseContent Upload(List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return new WebResponseContent().Error("请选择要上传的文件");
            }
            var response = new WebResponseContent(true);
            var savedDocuments = new List<object>();

            try
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
                    string fileHash = FileHashHelper.CalculateFileHash(file);

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

                // 使用Minio文件存储服务进行上传
                var uploadResult = _fileStorageService.UploadFiles(validFiles, "");

                if (uploadResult.Status)
                {
                    var uploadData = uploadResult.Data as List<object>;
                    if (uploadData != null && uploadData.Count > 0)
                    {
                        return new WebResponseContent().OK("文件上传成功", uploadData);
                    }
                }

                return uploadResult;
            }
            catch (Exception ex)
            {
                Logger.Error($"上传文件失败：{typeof(DMS_FileStorage).GetEntityTableCnName()},{ex.Message + ex.StackTrace}");
                return new WebResponseContent().Error("文件上传失败");
            }

        }
    }
}
