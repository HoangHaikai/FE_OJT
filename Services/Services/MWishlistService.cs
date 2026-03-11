using AdapterPattern.Adapter;
using BuilderPattern.Builder;
using Domain.DTO;
using Domain.Entities;
using Repositories.InterfaceRepositories;
using Services.InterfaceServices;

namespace Services.Services
{
    public class MWishlistService : IMWishlistService
    {
        private readonly IMWishlistRepository _mwishlistRepository;
        public MWishlistService(IMWishlistRepository mwishlistRepository)
        {
            _mwishlistRepository = mwishlistRepository;
        }

        public Mwishlist CreateWishlistAsync(CreateWishlistRequest request)
        {
            var wishlistId = Guid.NewGuid().ToString();
            var wishlist = new WishlistBuilder()
                .WithWishlistId(wishlistId)
                .WithUserId(request.UserId!)
                .WithProductId(request.ProductId!)
                .WithListingId(request.ListingId!)
                .WithNote(request.Note!)
                .Active()
                .Build();
            _mwishlistRepository.Create(wishlist);
            return wishlist;
        }

    }
}
