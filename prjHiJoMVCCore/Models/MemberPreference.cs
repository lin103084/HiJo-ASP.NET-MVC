using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class MemberPreference
{
    public int Id { get; set; }

    public int? MemberId { get; set; }

    public string LookingForSex { get; set; } = null!;

    public int? AgeRangeMin { get; set; }

    public int? AgeRangeMax { get; set; }

    public virtual Member? Member { get; set; }
}
