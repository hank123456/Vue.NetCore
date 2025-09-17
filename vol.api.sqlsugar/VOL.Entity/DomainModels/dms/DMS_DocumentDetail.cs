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
    [Entity(TableCnName = "文件源管理",TableName = "DMS_DocumentDetail")]
    public partial class DMS_DocumentDetail:BaseEntity
    {
        /// <summary>
       ///详情id
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="详情id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid Id { get; set; }

       /// <summary>
       ///文件id
       /// </summary>
       [Display(Name ="文件id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=true)]
       public Guid DocId { get; set; }

       /// <summary>
       ///文件名
       /// </summary>
       [Display(Name ="文件名")]
       [MaxLength(200)]
       [Column(TypeName="string(200)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string FileName { get; set; }

       /// <summary>
       ///文件大小
       /// </summary>
       [Display(Name ="文件大小")]
       [Column(TypeName="long")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long FileSize { get; set; }

       /// <summary>
       ///扩展名
       /// </summary>
       [Display(Name ="扩展名")]
       [MaxLength(20)]
       [Column(TypeName="string(20)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Extension { get; set; }

       /// <summary>
       ///类型
       /// </summary>
       [Display(Name ="类型")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string ContentType { get; set; }

       /// <summary>
       ///主版本号
       /// </summary>
       [Display(Name ="主版本号")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Major { get; set; }

       /// <summary>
       ///小版本号
       /// </summary>
       [Display(Name ="小版本号")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Minor { get; set; }

       /// <summary>
       ///修正版本号
       /// </summary>
       [Display(Name ="修正版本号")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Revision { get; set; }

       /// <summary>
       ///版本号
       /// </summary>
       [Display(Name ="版本号")]
       [MaxLength(20)]
       [Column(TypeName="string(20)")]
       [Editable(false)]
       [SugarColumn(IsOnlyIgnoreInsert = true, IsOnlyIgnoreUpdate = true)]// 添加这个属性，告诉SqlSugar在插入和更新时忽略此字段
        public string Version { get; set; }

       /// <summary>
       ///哈希值
       /// </summary>
       [Display(Name ="哈希值")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Hash { get; set; }

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
       ///当前状态
       /// </summary>
       [Display(Name ="当前状态")]
       [Column(TypeName="short")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public short CurrentState { get; set; }

       /// <summary>
       ///审核人id
       /// </summary>
       [Display(Name ="审核人id")]
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
       ///审核人
       /// </summary>
       [Display(Name ="审核人")]
       [MaxLength(50)]
       [Column(TypeName="string(50)")]
       [Editable(true)]
       public string Auditor { get; set; }

       /// <summary>
       ///审核时间
       /// </summary>
       [Display(Name ="审核时间")]
       [Column(TypeName="DateTime")]
       [Editable(true)]
       public DateTime? AuditDate { get; set; }

       /// <summary>
       ///审核理由
       /// </summary>
       [Display(Name ="审核理由")]
       [MaxLength(500)]
       [Column(TypeName="string(500)")]
       [Editable(true)]
       public string AuditReason { get; set; }

       /// <summary>
       ///下载次数
       /// </summary>
       [Display(Name ="下载次数")]
       [Column(TypeName="long")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long DownloadCount { get; set; }

       /// <summary>
       ///是否删除
       /// </summary>
       [Display(Name ="是否删除")]
       [Column(TypeName="bool")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public bool IsDeleted { get; set; }

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
       [Column(TypeName= "datetime")]
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
       [Column(TypeName= "datetime")]
       [Editable(true)]
       public DateTime? ModifyDate { get; set; }

       /// <summary>
       ///是否启用
       /// </summary>
       [Display(Name ="是否启用")]
       [Column(TypeName="short")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public short Enable { get; set; }

       
    }
}