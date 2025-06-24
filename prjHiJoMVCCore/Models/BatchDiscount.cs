using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class BatchDiscount
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public int DiscountId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Batch Batch { get; set; } = null!;

    public virtual Discount Discount { get; set; } = null!;
}
