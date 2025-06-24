using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MemberRole> MemberRoles { get; set; } = new List<MemberRole>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
