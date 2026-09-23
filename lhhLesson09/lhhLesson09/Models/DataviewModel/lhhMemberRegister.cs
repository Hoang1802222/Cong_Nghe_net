using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lhhLesson09.Models.DataviewModel
{
    /// <summary>
    /// Data Annotation - Validation
    /// </summary>
    public class lhhMemberRegister
    {
        public int lhhMemberId { get; set; }
        [DisplayName("Ten dang nhap")]
        [Required(ErrorMessage = "Ten dang nhap khong duoc de trong")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Ten dang nhap phai tu 5 den 20 ky tu")]
        public string lhhMemberName { get; set; }
        [DisplayName("Mat khau")]
        [Required(ErrorMessage = "Mat khau khong duoc de trong")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mat khau phai tu 6 den 100 ky tu")]
        [DataType(DataType.Password)]
        public string lhhMemberPassword { get; set; }
        public string lhhMemberEmail { get; set; }
        public string lhhMemberPhone { get; set; }
        public string lhhFullname { get; set; }
        public DateTime lhhBirtday { get; set; }
    }
}
