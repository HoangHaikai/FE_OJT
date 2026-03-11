using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Minspectionreport
{
    public string ReportId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string InspectorId { get; set; } = null!;

    public DateTime? InspectionDate { get; set; }

    public string? OverallRating { get; set; }

    public int? FrameCondition { get; set; }

    public string? FrameNotes { get; set; }

    public int? BrakeCondition { get; set; }

    public string? BrakeNotes { get; set; }

    public int? DrivetrainCondition { get; set; }

    public string? DrivetrainNotes { get; set; }

    public int? WheelCondition { get; set; }

    public string? WheelNotes { get; set; }

    public string? ImagesUrl { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Muser Inspector { get; set; } = null!;

    public virtual Mproduct Product { get; set; } = null!;
}
