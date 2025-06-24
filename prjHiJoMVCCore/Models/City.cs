using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public byte LocationLevel { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
