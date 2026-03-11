using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MdisputeEvidence
{
    public string EvidenceId { get; set; } = null!;

    public string DisputeId { get; set; } = null!;

    public string SubmittedBy { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Mdispute Dispute { get; set; } = null!;

    public virtual Muser SubmittedByNavigation { get; set; } = null!;
}
