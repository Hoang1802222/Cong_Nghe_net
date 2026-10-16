using System;
using System.Collections.Generic;

namespace nhom18.Models;

public partial class DanhMucMon
{
    public int MaDm { get; set; }

    public string TenDm { get; set; } = null!;

    public int ThuTu { get; set; }

    public virtual ICollection<Mon> Mons { get; set; } = new List<Mon>();
}
