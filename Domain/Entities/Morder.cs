using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Morder
{
    public string OrderId { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string? ShippingAddress { get; set; }

    public string? ReceiverName { get; set; }

    public string? ReceiverPhone { get; set; }

    public int? DeliveryMethod { get; set; }

    public int? OrderStatus { get; set; }

    public int? PaymentStatus { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public virtual Muser Buyer { get; set; } = null!;

    public virtual ICollection<Mdispute> Mdisputes { get; set; } = new List<Mdispute>();

    public virtual ICollection<Morderdetail> Morderdetails { get; set; } = new List<Morderdetail>();

    public virtual ICollection<Mpayment> Mpayments { get; set; } = new List<Mpayment>();

    public virtual Muser Seller { get; set; } = null!;
}
