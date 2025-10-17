/*
 *所有关于DMS_Document类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*DMS_DocumentService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Linq.Expressions;
using VOL.Core.BaseProvider;
using VOL.Core.DBManager;
using VOL.Core.Extensions;
using VOL.Core.Extensions.AutofacManager;
using VOL.Core.Utilities;
using VOL.DMS.IRepositories;
using VOL.Entity.DomainModels;

namespace VOL.DMS.Services
{
    public partial class DMS_DocumentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDMS_DocumentRepository _repository;//访问数据库

        [ActivatorUtilitiesConstructor]
        public DMS_DocumentService(
            IDMS_DocumentRepository dbRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            AddOnExecute = (SaveModel model) =>
            {
                string prefix = model.MainData["DocType"]?.ToString();
                string today = DateTime.Now.ToString("yyMMdd");

                // 查询当天的最大DocCode
                string maxCode = repository.FindAsIQueryable(x => x.DocType == prefix && x.DocCode.StartsWith($"{prefix}{today}"))
                    .OrderByDescending(x => x.DocCode)
                    .Select(x => x.DocCode)
                    .FirstOrDefault();

                string rule = $"{prefix}-{today}-";
                if (string.IsNullOrEmpty(maxCode) || maxCode.Length < rule.Length + 4)
                {
                    rule += "0001";
                }
                else
                {
                    int lastNumber = int.Parse(maxCode.Substring(rule.Length, 4));
                    rule += (lastNumber + 1).ToString("D4");
                }

                model.MainData["DocCode"] = rule;
                return new WebResponseContent().OK();
            };

            return base.Add(saveDataModel);
        }


    }
}
