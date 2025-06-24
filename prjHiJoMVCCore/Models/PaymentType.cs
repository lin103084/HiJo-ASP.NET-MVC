using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class PaymentType
{
    public int PaymentTypeId { get; set; }

    public string PaymentType1 { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
