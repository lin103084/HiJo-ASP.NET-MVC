using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class VipMember
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public int? VipLevel { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Member Member { get; set; } = null!;
}
