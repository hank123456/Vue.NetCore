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
    [Entity(TableCnName = "我的文档",TableName = "DMS_Document",DetailTable =  new Type[] { typeof(DMS_DocumentFile)},DetailTableCnName = "文档文件关联")]
    public partial class DMS_Document:BaseEntity
    {
        /// <summary>
       ///文档id
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="文档id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid DocumentId { get; set; }

       /// <summary>
       ///文档类型
       /// </summary>
       [Display(Name ="文档类型")]
       [MaxLength(50)]
       [Column(TypeName="string(50)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DocType { get; set; }

       /// <summary>
       ///文档编码
       /// </summary>
       [Display(Name ="文档编码")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DocCode { get; set; }

       /// <summary>
       ///文档名称
       /// </summary>
       [Display(Name ="文档名称")]
       [MaxLength(200)]
       [Column(TypeName="string(200)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DocName { get; set; }

       /// <summary>
       ///版本
       /// </summary>
       [Display(Name ="版本")]
       [MaxLength(20)]
       [Column(TypeName="string(20)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Version { get; set; }

       /// <summary>
       ///状态
       /// </summary>
       [Display(Name ="状态")]
       [MaxLength(20)]
       [Column(TypeName="string(20)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Status { get; set; }

       /// <summary>
       ///文档说明
       /// </summary>
       [Display(Name ="文档说明")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Editable(true)]
       public string Description { get; set; }

       /// <summary>
       ///是否可用
       /// </summary>
       [Display(Name ="是否可用")]
       [Column(TypeName="short")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public short Enable { get; set; }

       /// <summary>
       ///审核id
       /// </summary>
       [Display(Name ="审核id")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? AuditId { get; set; }

       /// <summary>
       ///审核状态
       /// </summary>
       [Display(Name ="审核状态")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? AuditStatus { get; set; }

       /// <summary>
       ///审核人
       /// </summary>
       [Display(Name ="审核人")]
       [MaxLength(200)]
       [Column(TypeName="string(200)")]
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
       ///审核备注
       /// </summary>
       [Display(Name ="审核备注")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
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
       public DateTime ModifyDate { get; set; }

       [Display(Name ="文档文件关联")]
       [ForeignKey("DocumentId")]
       [Navigate(NavigateType.OneToMany,nameof(DocumentId),nameof(DocumentId))]
       public List<DMS_DocumentFile> DMS_DocumentFile { get; set; }

    }
}