using Domain.Data;
using Domain.Entities;
using Repositories.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MWishlistRepository : RepositoryBase<Mwishlist>, IMWishlistRepository
    {
        public MWishlistRepository(AppDbContext context) : base (context)
        {
            
        }

        public List<Mwishlist> GetAllWishlist(Mwishlist mwishlist)
        {
            return GetAll().ToList();
        }

        public Mwishlist Create(Mwishlist mwishlist) { 
            var mwishlistCreated = new Mwishlist
            {
                WishlistId = mwishlist.WishlistId,
                UserId = mwishlist.UserId,
                ProductId = mwishlist.ProductId,
                ListingId = mwishlist.ListingId,
                Note = mwishlist.Note,
                CreatedAt = DateTime.Now,
                DeleteFlag = 0
            };
            base.Create(mwishlistCreated);
            return mwishlistCreated;
        }
    }
}
