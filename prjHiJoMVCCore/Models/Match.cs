using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Match
{
    public int Id { get; set; }

    public int UserId1 { get; set; }

    public int UserId2 { get; set; }

    public DateTime MatchedAt { get; set; }

    public int Status { get; set; }

    public virtual Member UserId1Navigation { get; set; } = null!;

    public virtual Member UserId2Navigation { get; set; } = null!;
}
