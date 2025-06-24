using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class EventPhoto
{
    public int EventPhotoId { get; set; }

    public int EventId { get; set; }

    public bool IsCover { get; set; }

    public string Photo { get; set; } = null!;

    public int SortOrder { get; set; }

    public virtual Event Event { get; set; } = null!;
}
