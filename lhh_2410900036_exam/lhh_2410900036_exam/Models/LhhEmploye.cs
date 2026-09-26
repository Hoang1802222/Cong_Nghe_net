using System;
using System.Collections.Generic;

namespace lhh_2410900036_exam.Models;

public partial class LhhEmploye
{
    public int Id { get; set; }

    public string LhhName { get; set; } = null!;

    public string? LhhGender { get; set; }

    public DateOnly? LhhBirthDay { get; set; }

    public string? LhhEmail { get; set; }

    public string? LhhPhone { get; set; }

    public bool? LhhActive { get; set; }
}
