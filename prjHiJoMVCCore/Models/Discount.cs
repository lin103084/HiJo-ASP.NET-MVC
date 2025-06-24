using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string DiscountInfo { get; set; } = null!;

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public virtual ICollection<BatchDiscount> BatchDiscounts { get; set; } = new List<BatchDiscount>();
}
