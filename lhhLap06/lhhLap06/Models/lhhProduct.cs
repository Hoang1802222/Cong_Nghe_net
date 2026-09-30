using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace lhhLap06.Models
{
    [Table("lhhProduct")]
    public class lhhProduct
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "tên sản phẩm không được để trống")]
        [StringLength(150, ErrorMessage = "ten san pham khong duoc qua 150 ky tu")]
        [Column(TypeName = "varchar(150)")]
        public string? Image { get; set; }
        [Required(ErrorMessage = "gia san pham khong duoc de trong")]
        public float Price { get; set; }
        public byte Status { get; set; }
        [StringLength(1000, ErrorMessage = "noi dung mo ta gioi han 1000 ky tu")]
        [Column(TypeName = "ntext")]
        public string? Descriptions { get; set; }
        [Required(ErrorMessage = "Danh muc san pham khong duoc de trong")]
        public int CategryId { get; set; }
        public DateTime CreatedDate { get; set; }
        //khoa ngoai voi bang category
        public lhhCategory category { get; set; } = null!;
    } 
}
