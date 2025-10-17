/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using Dm;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOL.Entity.SystemModels;

namespace VOL.Entity.DomainModels
{
    [Entity(TableCnName = "文件归档",TableName = "DMS_FileRecord",DetailTable =  new Type[] { typeof(DMS_FileVersion)},DetailTableCnName = "文件版本详情")]
    public partial class DMS_FileRecord:BaseEntity
    {
        /// <summary>
       ///文件记录ID
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="文件记录ID")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid FileRecordId { get; set; }

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
       ///详情描述
       /// </summary>
       [Display(Name ="详情描述")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Editable(true)]
       public string FileDescription { get; set; }

       /// <summary>
       ///文件分类
       /// </summary>
       [Display(Name ="文件分类")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       public Guid? CatalogID { get; set; }

       /// <summary>
       ///状态
       /// </summary>
       [Display(Name ="状态")]
       [Column(TypeName="short")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public short MasterState { get; set; }

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

       /// <summary>
       ///是否可用
       /// </summary>
       [Display(Name ="是否可用")]
       [Column(TypeName="short")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public short Enable { get; set; }

       [Display(Name ="文件版本详情")]
       [ForeignKey("FileRecordId")]
       [Navigate(NavigateType.OneToMany,nameof(FileRecordId),nameof(FileRecordId))]
       public List<DMS_FileVersion> DMS_FileVersion { get; set; }

    }
}