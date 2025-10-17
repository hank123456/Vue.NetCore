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
    [Entity(TableCnName = "归档文件明细",TableName = "DMS_FileVersion")]
    public partial class DMS_FileVersion:BaseEntity
    {
        /// <summary>
       ///文件版本id
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="文件版本id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid FileVersionId { get; set; }

       /// <summary>
       ///文件归档id
       /// </summary>
       [Display(Name ="文件归档id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid FileRecordId { get; set; }

       /// <summary>
       ///文件名
       /// </summary>
       [Display(Name ="文件名")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Required(AllowEmptyStrings=false)]
       public string FileRecordName { get; set; }

       /// <summary>
       ///主版本
       /// </summary>
       [Display(Name ="主版本")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Major { get; set; }

       /// <summary>
       ///小版本
       /// </summary>
       [Display(Name ="小版本")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Minor { get; set; }

       /// <summary>
       ///版本
       /// </summary>
       [Display(Name ="版本")]
       [MaxLength(20)]
       [Column(TypeName="string(20)")]
       [Editable(true)]
       public string Version { get; set; }

       /// <summary>
       ///文件大小
       /// </summary>
       [Display(Name ="文件大小")]
       [Column(TypeName="long")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long FileSize { get; set; }

       /// <summary>
       ///文件类型
       /// </summary>
       [Display(Name ="文件类型")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string ContentType { get; set; }

       /// <summary>
       ///文件后缀
       /// </summary>
       [Display(Name ="文件后缀")]
       [MaxLength(20)]
       [Column(TypeName="string(20)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Extension { get; set; }

       /// <summary>
       ///Hash值
       /// </summary>
       [Display(Name ="Hash值")]
       [MaxLength(1024)]
       [Column(TypeName="string (1024)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Hash { get; set; }

       /// <summary>
       ///更改原因
       /// </summary>
       [Display(Name ="更改原因")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Editable(true)]
       public string ChangeReason { get; set; }

       /// <summary>
       ///Minio桶
       /// </summary>
       [Display(Name ="Minio桶")]
       [MaxLength(63)]
       [Column(TypeName="string(63)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string MinioBucket { get; set; }

       /// <summary>
       ///Minio对象
       /// </summary>
       [Display(Name ="Minio对象")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string MinioObject { get; set; }

       /// <summary>
       ///审核者id
       /// </summary>
       [Display(Name ="审核者id")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? AuditId { get; set; }

       /// <summary>
       ///审核状态
       /// </summary>
       [Display(Name ="审核状态")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int AuditStatus { get; set; }

       /// <summary>
       ///审核者
       /// </summary>
       [Display(Name ="审核者")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string Auditor { get; set; }

       /// <summary>
       ///审核时间
       /// </summary>
       [Display(Name ="审核时间")]
       [MaxLength(1024)]
       [Column(TypeName= "dateTime")]
       [Editable(true)]
       public DateTime AuditDate { get; set; }

       /// <summary>
       ///审批记录
       /// </summary>
       [Display(Name ="审批记录")]
       [MaxLength(500)]
       [Column(TypeName="string(500)")]
       [Editable(true)]
       public string AuditReason { get; set; }

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