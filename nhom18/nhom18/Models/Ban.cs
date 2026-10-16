using System;
using System.Collections.Generic;

namespace nhom18.Models;

public partial class Ban
{
    public int MaBan { get; set; }

    public string TenBan { get; set; } = null!;

    public string KhuVuc { get; set; } = null!;

    public int SoGhe { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
