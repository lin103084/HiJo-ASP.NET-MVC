using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class MemberRole
{
    public int MemberId { get; set; }

    public int RoleId { get; set; }

    public int Id { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
