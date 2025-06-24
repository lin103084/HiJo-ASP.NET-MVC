using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int EventTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CityId { get; set; }

    public string Address { get; set; } = null!;

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual City City { get; set; } = null!;

    public virtual ICollection<EventPhoto> EventPhotos { get; set; } = new List<EventPhoto>();

    public virtual EventType EventType { get; set; } = null!;
}
