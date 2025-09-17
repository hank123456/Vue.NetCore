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
    [Entity(TableCnName = "我的文件",TableName = "DMS_Document",DetailTable =  new Type[] { typeof(DMS_DocumentDetail)},DetailTableCnName = "文件详情")]
    public partial class DMS_Document:BaseEntity
    {
        /// <summary>
       ///文件id
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="文件id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid DocId { get; set; }

       /// <summary>
       ///产品代码
       /// </summary>
       [Display(Name ="产品代码")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string ProductCode { get; set; }

       /// <summary>
       ///文件类型
       /// </summary>
       [Display(Name ="文件类型")]
       [MaxLength(20)]
       [Column(TypeName="string(20)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DocType { get; set; }

       /// <summary>
       ///文件名
       /// </summary>
       [Display(Name ="文件名")]
       [MaxLength(200)]
       [Column(TypeName="string(200)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Name { get; set; }

       /// <summary>
       ///最新版本id
       /// </summary>
       [Display(Name ="最新版本id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       public Guid? LastVersionId { get; set; }

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
       ///创建日期
       /// </summary>
       [Display(Name ="创建日期")]
       [MaxLength(1024)]
       [Column(TypeName="datetime")]
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
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime ModifyDate { get; set; }

       /// <summary>
       ///是否可用
       /// </summary>
       [Display(Name ="是否可用")]
       [Column(TypeName="short")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public short Enable { get; set; }

       [Display(Name ="文件详情")]
       [ForeignKey("DocId")]
       [Navigate(NavigateType.OneToMany,nameof(DocId),nameof(DocId))]
       public List<DMS_DocumentDetail> DMS_DocumentDetail { get; set; }

    }
}