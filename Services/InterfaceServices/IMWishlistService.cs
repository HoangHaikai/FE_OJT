using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServices
{
    public interface IMWishlistService
    {
        Mwishlist CreateWishlistAsync(CreateWishlistRequest request);
        
    }
}
