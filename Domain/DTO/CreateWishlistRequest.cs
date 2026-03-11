using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class CreateWishlistRequest
    {
            public string? UserId { get; set; }
            public string? ProductId { get; set; }
            public string? ListingId { get; set; }
            public string? Note { get; set; }
        
    }
}
