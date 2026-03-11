using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Morderdetail
{
    public string OrderDetailId { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int? Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Morder Order { get; set; } = null!;

    public virtual Mproduct Product { get; set; } = null!;
}
