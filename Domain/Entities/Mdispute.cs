using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mdispute
{
    public string DisputeId { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public int Reason { get; set; }

    public string Description { get; set; } = null!;

    public string? EvidenceUrls { get; set; }

    public int Status { get; set; }

    public int? Resolution { get; set; }

    public decimal? RefundAmount { get; set; }

    public string? ResolvedBy { get; set; }

    public string? AdminNote { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public virtual Muser Buyer { get; set; } = null!;

    public virtual ICollection<MdisputeEvidence> MdisputeEvidences { get; set; } = new List<MdisputeEvidence>();

    public virtual Morder Order { get; set; } = null!;

    public virtual Muser? ResolvedByNavigation { get; set; }

    public virtual Muser Seller { get; set; } = null!;
}
