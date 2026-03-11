using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Mwishlist
{
    public string WishlistId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string? ListingId { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? DeleteFlag { get; set; }

    public virtual Mlisting? Listing { get; set; }

    public virtual Mproduct Product { get; set; } = null!;

    public virtual Muser User { get; set; } = null!;
    public Mwishlist()
    {
         
    }
    public Mwishlist(string wishlistId, string userId, string productId, string? listingId, string? note, DateTime? createdAt, int? deleteFlag)
    {
        WishlistId = wishlistId;
        UserId = userId;
        ProductId = productId;
        ListingId = listingId;
        Note = note;
        CreatedAt = createdAt;
        DeleteFlag = deleteFlag;
    }
}
