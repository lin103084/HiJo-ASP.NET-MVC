using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class MemberInterest
{
    public int Id { get; set; }

    public int? MemberId { get; set; }

    public int? InterestId { get; set; }

    public virtual Interest? Interest { get; set; }

    public virtual Member? Member { get; set; }
}
