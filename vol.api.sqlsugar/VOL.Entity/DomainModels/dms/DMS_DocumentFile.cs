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
    [Entity(TableCnName = "文档文件关联",TableName = "DMS_DocumentFile")]
    public partial class DMS_DocumentFile:BaseEntity
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
       ///序号
       /// </summary>
       [Display(Name ="序号")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int Sequence { get; set; }

       /// <summary>
       ///文档id
       /// </summary>
       [Display(Name ="文档id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid DocumentId { get; set; }

       /// <summary>
       ///文件id
       /// </summary>
       [Display(Name ="文件id")]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public Guid FileId { get; set; }

       /// <summary>
       ///文件类型
       /// </summary>
       [Display(Name ="文件类型")]
       [MaxLength(50)]
       [Column(TypeName="string(50)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string FileType { get; set; }

       /// <summary>
       ///文件可用
       /// </summary>
       [Display(Name ="文件可用")]
       [Column(TypeName="short")]
       [Editable(true)]
       public short? Enable { get; set; }

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
       ///编辑者id
       /// </summary>
       [Display(Name ="编辑者id")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? ModifyID { get; set; }

       /// <summary>
       ///编辑者
       /// </summary>
       [Display(Name ="编辑者")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       [Editable(true)]
       public string Modifier { get; set; }

       /// <summary>
       ///编辑时间
       /// </summary>
       [Display(Name ="编辑时间")]
       [MaxLength(1024)]
       [Column(TypeName= "datetime")]
       [Editable(true)]
       public DateTime ModifyDate { get; set; }

        [Display(Name = "文件信息")]
        [ForeignKey("FileId")]
        [Navigate(NavigateType.ManyToOne, nameof(FileId), nameof(FileId))]
        public DMS_File DMS_File { get; set; }
    }
}