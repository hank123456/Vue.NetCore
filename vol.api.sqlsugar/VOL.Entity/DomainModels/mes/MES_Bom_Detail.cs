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
    [Entity(TableCnName = "BOM明细",TableName = "MES_Bom_Detail",DBServer = "ServiceDbContext")]
    public partial class MES_Bom_Detail:BaseEntity
    {
        /// <summary>
       ///单台用量
       /// </summary>
       [Display(Name ="单台用量")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public decimal UsageQty { get; set; }

       /// <summary>
       ///
       /// </summary>
       [SugarColumn(IsPrimaryKey = true)]
       [Key]
       [Display(Name ="DomDetailId")]
       [MaxLength(36)]
       [Column(TypeName= "uniqueidentifier")]
       [Required(AllowEmptyStrings=false)]
       public Guid DomDetailId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="BomId")]
       [MaxLength(36)]
       [Column(TypeName= "uniqueidentifier")]
       [Editable(true)]
        public Guid BomId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Sequence")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
        [Editable(true)]
        public int Sequence { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="MaterialCode")]
       [MaxLength(200)]
       [Column(TypeName="string(200)")]
       public string MaterialCode { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="MaterialName")]
       [MaxLength(1024)]
       [Column(TypeName="string(1024)")]
       public string MaterialName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Spec")]
       [MaxLength(200)]
       [Column(TypeName="string(200)")]
       public string Spec { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ConsumeModel")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       public string ConsumeModel { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Warehouse")]
       [MaxLength(100)]
       [Column(TypeName="string(100)")]
       public string Warehouse { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="SupplierCode")]
       [MaxLength(50)]
       [Column(TypeName="string(50)")]
       public string SupplierCode { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="KitScale")]
       [Column(TypeName="decimal")]
       public decimal? KitScale { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Enable")]
       [Column(TypeName="int")]
       public int? Enable { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateID")]
       [Column(TypeName="int")]
       public int? CreateID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Creator")]
       [MaxLength(50)]
       [Column(TypeName="string(50)")]
       public string Creator { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateDate")]
       [MaxLength(1024)]
       [Column(TypeName= "datetime")]
       public DateTime? CreateDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ModifyID")]
       [Column(TypeName="int")]
       public int? ModifyID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Modifier")]
       [MaxLength(50)]
       [Column(TypeName="string(50)")]
       public string Modifier { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ModifyDate")]
       [MaxLength(1024)]
       [Column(TypeName= "datetime")]
       public DateTime? ModifyDate { get; set; }

       
    }
}