using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mpayment
{
    public string PaymentId { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public int? PaymentMethod { get; set; }

    public int? PaymentType { get; set; }

    public decimal Amount { get; set; }

    public string? TransactionCode { get; set; }

    public string? PaymentGateway { get; set; }

    public int? PaymentStatus { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? RefundedAt { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Morder Order { get; set; } = null!;
}
