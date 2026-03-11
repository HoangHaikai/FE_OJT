using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mrole
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Muser> Musers { get; set; } = new List<Muser>();
}
