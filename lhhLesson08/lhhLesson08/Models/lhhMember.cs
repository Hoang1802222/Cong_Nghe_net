using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lhhLesson08.Models
{
    public class lhhMember
    {
        public string lhhMemberID { get; set; }
        public string lhhMemberName { get; set; }
        public string lhhMemberEmail { get; set; }
        
        public string lhhMemberPassword { get; set; }
        [DisplayName("Số điện thoại")]
        public string lhhMemberPhone { get; set; }
    }
}
