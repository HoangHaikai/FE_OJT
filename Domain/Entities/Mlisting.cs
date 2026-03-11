using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mlisting
{
    public string ListingId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? FeaturedImage { get; set; }

    public int? Status { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public virtual Muser? ApprovedByNavigation { get; set; }

    public virtual ICollection<Mwishlist> Mwishlists { get; set; } = new List<Mwishlist>();

    public virtual Mproduct Product { get; set; } = null!;

    public virtual Muser Seller { get; set; } = null!;
}
