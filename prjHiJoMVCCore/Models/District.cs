using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int ParentCityId { get; set; }

    public byte LocationLevel { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual City ParentCity { get; set; } = null!;
}
