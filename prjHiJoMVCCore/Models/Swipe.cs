using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Swipe
{
    public int Id { get; set; }

    public int SwiperId { get; set; }

    public int TargetId { get; set; }

    public int? SwipesAction { get; set; }

    public DateTime? CreaTime { get; set; }

    
    public virtual Member Swiper { get; set; } = null!;

    public virtual Member Target { get; set; } = null!;
}
