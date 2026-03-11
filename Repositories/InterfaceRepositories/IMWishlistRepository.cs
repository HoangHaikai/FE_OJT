using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceRepositories
{
    public interface IMWishlistRepository
    {
        Mwishlist Create(Mwishlist mwishlist);
        List<Mwishlist> GetAllWishlist(Mwishlist mwishlist);
    }
}
