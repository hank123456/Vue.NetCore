﻿/*
 *Author：jxx
 *Contact：xx
 *Date：2018-07-01
 * 此代码由框架生成，请勿随意更改
 */
using VOL.Sys.IRepositories;
using VOL.Sys.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.Sys.Services
{
    public partial class Sys_DictionaryService : ServiceBase<Sys_Dictionary, ISys_DictionaryRepository>, ISys_DictionaryService, IDependency
    {
        public Sys_DictionaryService(ISys_DictionaryRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static ISys_DictionaryService Instance
        {
           get { return AutofacContainerModule.GetService<ISys_DictionaryService>(); }
        }
    }
}

