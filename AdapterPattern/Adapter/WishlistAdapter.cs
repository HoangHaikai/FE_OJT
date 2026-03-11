using AdapterPattern.IAdapter;
using Domain.Data;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdapterPattern.Adapter
{
    public class WishlistAdapter : IWishlistAdapter
    {
        private readonly JsonWishlist _context;
        public WishlistAdapter(JsonWishlist context)
        {
            _context = context;
        }

        public List<Mwishlist> GetAllMwishlists()
        {
            string jsonData = _context.GetWishlistsAsJson();

            var wishListBicycles = JsonSerializer.Deserialize<List<Mwishlist>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            return wishListBicycles;
        }
    }
}
