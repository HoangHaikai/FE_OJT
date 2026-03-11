using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern.InterfaceBuilder
{
    public interface IWishlistBuilder
    {
        IWishlistBuilder WithWishlistId(string wishlistId);
        IWishlistBuilder WithUserId(string userId);
        IWishlistBuilder WithProductId(string productId);
        IWishlistBuilder WithListingId(string listingId);
        IWishlistBuilder WithNote(string note);
        IWishlistBuilder Active();
        IWishlistBuilder Deleted();
        Mwishlist Build();
    }
}
