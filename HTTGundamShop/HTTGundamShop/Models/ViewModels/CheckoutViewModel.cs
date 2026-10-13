using System.ComponentModel.DataAnnotations;

namespace HTTGundamShop.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên người nhận")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string Address { get; set; } = string.Empty;

        public string? Note { get; set; }

        public List<CartItemViewModel> CartItems { get; set; } = new();
        public decimal GrandTotal => CartItems.Sum(x => x.Total);
    }
}