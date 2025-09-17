/*
 *所有关于DMS_FileStorage类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*DMS_FileStorageService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Services;
using VOL.Core.Utilities;
using VOL.DMS.IRepositories;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Services
{
    public partial class DMS_FileStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDMS_FileStorageRepository _repository;//访问数据库
        private readonly IFileStorageService _fileStorageService;

        [ActivatorUtilitiesConstructor]
        public DMS_FileStorageService(
            IDMS_FileStorageRepository dbRepository,
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
            // 检查FileGroupId是否为空，如果为空则设置新的Guid，否则使用传入的值
            if (!saveDataModel.MainData.ContainsKey("FileGroupId") ||
                saveDataModel.MainData["FileGroupId"] == null ||
                saveDataModel.MainData["FileGroupId"].ToString() == string.Empty ||
                (saveDataModel.MainData["FileGroupId"] is Guid guid && guid == Guid.Empty))
            {
                saveDataModel.MainData["FileGroupId"] = Guid.NewGuid();
            }
            return base.Add(saveDataModel);
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

            Guid commonFileGroupId = Guid.NewGuid();


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

        /// <summary>
        /// 上传文件并创建文件存储记录（对于选文件直接上传，然后生成记录有用。。不一定有用，先弄个再说）
        /// </summary>
        public WebResponseContent UploadAndCreateRecord(List<IFormFile> files, Guid? fileGroupId = null)
        {
            if (files == null || !files.Any())
            {
                return new WebResponseContent().Error("请选择要上传的文件");
            }

            var response = new WebResponseContent(true);
            var savedDocuments = new List<object>();
            Guid commonFileGroupId = fileGroupId ?? Guid.NewGuid();

            try
            {
                // 首先检查所有文件的hash值
                var fileHashMap = new Dictionary<IFormFile, string>();
                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        continue;
                    }

                    string fileHash = FileHashHelper.CalculateFileHash(file);
                    var existingFile = _repository.Find(x => x.Hash == fileHash && (x.Enable == 1)).FirstOrDefault();
                    if (existingFile != null)
                    {
                        return new WebResponseContent().Error($"文件 '{file.FileName}' 已存在重复的文件(Hash: {fileHash})，请检查是否为重复上传");
                    }

                    fileHashMap[file] = fileHash;
                }

                if (!fileHashMap.Any())
                {
                    return new WebResponseContent().Error("没有有效的文件可上传");
                }

                // 使用文件存储服务进行上传
                var uploadResult = _fileStorageService.UploadFiles(fileHashMap.Keys.ToList(), "");
                if (!uploadResult.Status)
                {
                    return uploadResult;
                }

                // 获取上传结果
                var uploadData = uploadResult.Data as List<object>;
                if (uploadData == null)
                {
                    return new WebResponseContent().Error("上传结果数据格式错误");
                }

                // 为每个上传的文件创建数据库记录
                var fileList = fileHashMap.Keys.ToList();
                for (int i = 0; i < uploadData.Count && i < fileList.Count; i++)
                {
                    var item = uploadData[i];
                    var file = fileList[i];
                    var fileHash = fileHashMap[file];

                    string? fileName = GetDynamicProperty(item, "FileName")?.ToString();
                    long fileSize = Convert.ToInt64(GetDynamicProperty(item, "FileSize") ?? 0);
                    string? contentType = GetDynamicProperty(item, "ContentType")?.ToString();
                    string? minioBucket = GetDynamicProperty(item, "MinioBucket")?.ToString();
                    string? minioObject = GetDynamicProperty(item, "MinioObject")?.ToString();
                    string? url = GetDynamicProperty(item, "Url")?.ToString();

                    if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(minioObject))
                    {
                        return new WebResponseContent().Error("上传结果缺少必要的文件信息");
                    }

                    var fileStorage = new DMS_FileStorage
                    {
                        Id = Guid.NewGuid(),
                        FileGroupId = commonFileGroupId,
                        FileName = fileName,
                        FileSize = fileSize,
                        Extension = Path.GetExtension(fileName),
                        ContentType = contentType,
                        Major = 1,
                        Minor = 0,
                        Revision = 0,
                        Hash = fileHash,
                        MinioBucket = minioBucket,
                        MinioObject = minioObject,
                        AuditStatus = 0,
                        Enable = 1
                    };

                    // 设置创建人信息
                    fileStorage.SetCreateDefaultVal();

                    // 保存到数据库
                    var addResult = AddEntity(fileStorage);
                    if (!addResult.Status)
                    {
                        // 如果数据库保存失败，删除已上传的文件
                        if (!string.IsNullOrEmpty(minioObject))
                        {
                            _fileStorageService.DeleteAsync(minioObject);
                        }
                        return addResult;
                    }

                    savedDocuments.Add(new
                    {
                        Id = fileStorage.Id,
                        FileGroupId = fileStorage.FileGroupId,
                        FileName = fileStorage.FileName,
                        FileSize = fileStorage.FileSize,
                        Url = url
                    });
                }

                return response.OK("文件上传成功", savedDocuments);
            }
            catch (Exception ex)
            {
                return response.Error($"文件上传失败：{ex.Message}");
            }
        }
        // <summary>
        /// 安全地获取动态对象的属性值
        /// </summary>
        private object GetDynamicProperty(object obj, string propertyName)
        {
            if (obj == null) return null;

            try
            {
                var type = obj.GetType();
                var property = type.GetProperty(propertyName);
                return property?.GetValue(obj);
            }
            catch
            {
                return null;
            }
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

            var url = _fileStorageService.GetFileUrl(document.MinioObject);
            return new WebResponseContent().OK("获取成功", new { Url = url });
        }
        
    }
}
