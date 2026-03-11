using BuilderPattern.InterfaceBuilder;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern.Builder
{
    public class WishlistBuilder : IWishlistBuilder
    {
        private string? WishlistId;
        private string? UserId;
        private string? ProductId;
        private string? ListingId;
        private string? Note;
        private int DeleteFlag = 0;
        
        public IWishlistBuilder Active()
        {
            DeleteFlag = 0;
            return this;
        }

        public Mwishlist Build()
        {
            if (string.IsNullOrEmpty(WishlistId))
                throw new ArgumentException("WishlistId is required");

            if (string.IsNullOrEmpty(UserId))
                throw new ArgumentException("UserId is required");

            if (string.IsNullOrEmpty(ProductId))
                throw new ArgumentException("ProductId is required");
            return new Mwishlist(
                WishlistId, 
                UserId, 
                ProductId,
                ListingId,
                Note,
                DateTime.Now,
                DeleteFlag
            );
        }

        public IWishlistBuilder Deleted()
        {
            DeleteFlag = 1;
            return this;
        }

        public IWishlistBuilder WithListingId(string listingId)
        {
            ListingId = listingId;
            return this;
        }

        public IWishlistBuilder WithNote(string note)
        {
            Note = note;
            return this;
        }

        public IWishlistBuilder WithProductId(string productId)
        {
            ProductId = productId;
            return this;
        }

        public IWishlistBuilder WithUserId(string userId)
        {
            UserId = userId;
            return this;
        }

        public IWishlistBuilder WithWishlistId(string wishlistId)
        {
            WishlistId = wishlistId;
            return this;
        }
    }
}
