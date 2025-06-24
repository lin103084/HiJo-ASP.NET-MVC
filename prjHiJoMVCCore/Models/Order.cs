using System;
using System.Collections.Generic;

namespace prjHiJoMVCCore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int MemberId { get; set; }

    public int BatchId { get; set; }

    public DateTime OrderDate { get; set; }

    public int PaymentTypeId { get; set; }

    public decimal TotalPrice { get; set; }

    public int OrderStatusId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public virtual Batch Batch { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual PaymentType PaymentType { get; set; } = null!;
}
