/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using VOL.Entity.SystemModels;

namespace VOL.Entity.DomainModels
{
    [Entity(TableCnName = "文件分类",TableName = "DMS_FileCatalog")]
    public partial class DMS_FileCatalog:BaseEntity
    {
        /// <summary>
       ///分类id
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="分类id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid CatalogID { get; set; }

       /// <summary>
       ///分类code
       /// </summary>
       [Display(Name ="分类code")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string CatalogCode { get; set; }

       /// <summary>
       ///分类名
       /// </summary>
       [Display(Name ="分类名")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string CatalogName { get; set; }

       /// <summary>
       ///分类型号
       /// </summary>
       [Display(Name ="分类型号")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string CatalogType { get; set; }

       /// <summary>
       ///父类id
       /// </summary>
       [Display(Name ="父类id")]
       [MaxLength(36)]
       [Column(TypeName="string(36)")]
       [Editable(true)]
       public string ParentId { get; set; }

       /// <summary>
       ///是否可用
       /// </summary>
       [Display(Name ="是否可用")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? Enable { get; set; }

       /// <summary>
       ///记录
       /// </summary>
       [Display(Name ="记录")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string Remarks { get; set; }

       /// <summary>
       ///创建者id
       /// </summary>
       [Display(Name ="创建者id")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? CreateID { get; set; }

       /// <summary>
       ///创建者
       /// </summary>
       [Display(Name ="创建者")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string Creator { get; set; }

       /// <summary>
       ///创建时间
       /// </summary>
       [Display(Name ="创建时间")]
       [MaxLength(1024)]
       [Column(TypeName= "dateTime")]
       [Editable(true)]
       public DateTime CreateDate { get; set; }

       /// <summary>
       ///修改者id
       /// </summary>
       [Display(Name ="修改者id")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? ModifyID { get; set; }

       /// <summary>
       ///修改者
       /// </summary>
       [Display(Name ="修改者")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string Modifier { get; set; }

       /// <summary>
       ///修改时间
       /// </summary>
       [Display(Name ="修改时间")]
       [MaxLength(1024)]
       [Column(TypeName= "dateTime")]
       [Editable(true)]
       public DateTime ModifyDate { get; set; }

       
    }
}