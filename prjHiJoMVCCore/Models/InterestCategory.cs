using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class InterestCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Interest> Interests { get; set; } = new List<Interest>();
}
