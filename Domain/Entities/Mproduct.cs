using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mproduct
{
    public string ProductId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string BrandId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public int? Condition { get; set; }

    public string? FrameSize { get; set; }

    public int? FrameMaterial { get; set; }

    public string? WheelSize { get; set; }

    public string? BrakeType { get; set; }

    public string? GearSystem { get; set; }

    public decimal? Weight { get; set; }

    public string? Color { get; set; }

    public int? YearOfManufacture { get; set; }

    public decimal? UsageHistory { get; set; }

    public decimal Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? LocationCity { get; set; }

    public int? ViewCount { get; set; }

    public int? Status { get; set; }

    public int? InspectionStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? SoldAt { get; set; }

    public int? DeleteFlag { get; set; }

    public virtual Mbrand Brand { get; set; } = null!;

    public virtual Mcategory Category { get; set; } = null!;

    public virtual ICollection<Minspectionreport> Minspectionreports { get; set; } = new List<Minspectionreport>();

    public virtual ICollection<Mlisting> Mlistings { get; set; } = new List<Mlisting>();

    public virtual ICollection<Morderdetail> Morderdetails { get; set; } = new List<Morderdetail>();

    public virtual ICollection<Mwishlist> Mwishlists { get; set; } = new List<Mwishlist>();

    public virtual Muser Seller { get; set; } = null!;
}
