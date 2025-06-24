using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Batch
{
    public int BatchId { get; set; }

    public int EventId { get; set; }

    public int Quota { get; set; }

    public decimal OriginalPrice { get; set; }

    public DateTime EventStartDate { get; set; }

    public DateTime EventEndDate { get; set; }

    public DateTime RegistrationStartDate { get; set; }

    public DateTime RegistrationEndDate { get; set; }

    public virtual ICollection<BatchDiscount> BatchDiscounts { get; set; } = new List<BatchDiscount>();

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
