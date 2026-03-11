using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Muser
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? FullName { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? Gender { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public string RoleId { get; set; } = null!;

    public virtual ICollection<Mdispute> MdisputeBuyers { get; set; } = new List<Mdispute>();

    public virtual ICollection<MdisputeEvidence> MdisputeEvidences { get; set; } = new List<MdisputeEvidence>();

    public virtual ICollection<Mdispute> MdisputeResolvedByNavigations { get; set; } = new List<Mdispute>();

    public virtual ICollection<Mdispute> MdisputeSellers { get; set; } = new List<Mdispute>();

    public virtual ICollection<Minspectionreport> Minspectionreports { get; set; } = new List<Minspectionreport>();

    public virtual ICollection<Mlisting> MlistingApprovedByNavigations { get; set; } = new List<Mlisting>();

    public virtual ICollection<Mlisting> MlistingSellers { get; set; } = new List<Mlisting>();

    public virtual ICollection<Morder> MorderBuyers { get; set; } = new List<Morder>();

    public virtual ICollection<Morder> MorderSellers { get; set; } = new List<Morder>();

    public virtual ICollection<Mproduct> Mproducts { get; set; } = new List<Mproduct>();

    public virtual ICollection<Mwishlist> Mwishlists { get; set; } = new List<Mwishlist>();

    public virtual Mrole Role { get; set; } = null!;
}
