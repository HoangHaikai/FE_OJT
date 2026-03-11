using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mbrand
{
    public string BrandId { get; set; } = null!;

    public string BrandName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Mproduct> Mproducts { get; set; } = new List<Mproduct>();
}
