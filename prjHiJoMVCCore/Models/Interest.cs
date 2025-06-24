using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Interest
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CategoryId { get; set; }

    public virtual InterestCategory? Category { get; set; }

    public virtual ICollection<MemberInterest> MemberInterests { get; set; } = new List<MemberInterest>();
}
