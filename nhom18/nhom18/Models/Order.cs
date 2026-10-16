using System;
using System.Collections.Generic;

namespace nhom18.Models;

public partial class Order
{
    public int MaOrder { get; set; }

    public int? MaBan { get; set; }

    public int? MaKh { get; set; }

    public string LoaiOrder { get; set; } = null!;

    public DateTime GioVao { get; set; }

    public DateTime? GioThanhToan { get; set; }

    public decimal TongTien { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<ChiTietOrder> ChiTietOrders { get; set; } = new List<ChiTietOrder>();

    public virtual Ban? MaBanNavigation { get; set; }
}
