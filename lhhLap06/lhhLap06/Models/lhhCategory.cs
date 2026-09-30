using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace lhhLap06.Models
{
    [Table("lhhCategory")]
    public class lhhCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }
        [Required(ErrorMessage = "tên không được để trống")]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(10)")]
        public string categoryName { get; set; } = string.Empty;
        [Column(TypeName ="nvarchar(100)")]
        public byte status { get; set; }
        public DateTime? CreatedDate { get; set; }
        //danh sach san pham theo danh muc
        public ICollection<lhhProduct> Products { get; set; } = new List<lhhProduct>();
    }
}
