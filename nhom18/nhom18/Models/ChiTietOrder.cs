using System;
using System.Collections.Generic;

namespace nhom18.Models;

public partial class ChiTietOrder
{
    public int MaOrder { get; set; }

    public int MaMon { get; set; }

    public int SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public string? GhiChu { get; set; }

    public virtual Mon MaMonNavigation { get; set; } = null!;

    public virtual Order MaOrderNavigation { get; set; } = null!;
}
