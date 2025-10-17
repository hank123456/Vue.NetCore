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
    [Entity(TableCnName = "已签文件管理",TableName = "DMS_FileApproved")]
    public partial class DMS_FileApproved:BaseEntity
    {
        /// <summary>
       ///ID
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="ID")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid Id { get; set; }

       /// <summary>
       ///签署文件id
       /// </summary>
       [Display(Name ="签署文件id")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string FileId { get; set; }

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
       ///大小
       /// </summary>
       [Display(Name ="大小")]
       [Column(TypeName="long")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long FileSize { get; set; }

       /// <summary>
       ///备注
       /// </summary>
       [Display(Name ="备注")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       [Editable(true)]
       public string FileNote { get; set; }

       /// <summary>
       ///后缀
       /// </summary>
       [Display(Name ="后缀")]
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
       ///哈希值
       /// </summary>
       [Display(Name ="哈希值")]
       [MaxLength(1024)]
       [Column(TypeName="string (1024)")]
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
       ///是否可用
       /// </summary>
       [Display(Name ="是否可用")]
       [Column(TypeName="short")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public short Enable { get; set; }

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
       public DateTime? CreateDate { get; set; }

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
       public DateTime? ModifyDate { get; set; }

       
    }
}