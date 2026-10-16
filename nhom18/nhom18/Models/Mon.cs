using System;
using System.Collections.Generic;

namespace nhom18.Models;

public partial class Mon
{
    public int MaMon { get; set; }

    public string TenMon { get; set; } = null!;

    public int MaDm { get; set; }

    public decimal GiaBan { get; set; }

    public string? AnhMon { get; set; }

    public string? MoTa { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<ChiTietOrder> ChiTietOrders { get; set; } = new List<ChiTietOrder>();

    public virtual DanhMucMon MaDmNavigation { get; set; } = null!;
}
