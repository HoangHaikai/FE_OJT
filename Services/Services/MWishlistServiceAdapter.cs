using AdapterPattern.Adapter;
using AdapterPattern.IAdapter;
using Domain.Entities;
using Repositories.InterfaceRepositories;
using Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class MWishlistServiceAdapter : IMWishlistServiceAdapter
    {
        private readonly IWishlistAdapter _mwishlistAdapter;
        public MWishlistServiceAdapter(IWishlistAdapter mwishlistAdapter)
        {
            _mwishlistAdapter = mwishlistAdapter;
        }

        public List<Mwishlist> GetAllMwishlists()
        {
            return _mwishlistAdapter.GetAllMwishlists();
        }
    }
}
