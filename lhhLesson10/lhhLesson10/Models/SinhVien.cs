using System;
using System.Collections.Generic;

namespace lhhLesson10.Models;

public partial class SinhVien
{
    public int MaSv { get; set; }

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? Email { get; set; }
}
