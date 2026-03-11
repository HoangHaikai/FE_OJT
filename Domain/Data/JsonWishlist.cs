using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class JsonWishlist
    {
        public string GetWishlistsAsJson()
        {
            return @"[{
                ""wishlistId"": ""W001"",
                ""User"": {""Id"": 1, ""FullName"": ""Viet""},
                ""Product"": {""Id"": ""P001"", ""ProductName"": ""Mountain Bike""},
                ""Listing"": {""Id"":""L001"", ""Title"": ""Sell Bike""},
                ""note"": ""Sản phẩm yêu thích"",
                ""createdAt"": ""2024-01-01T00:00:00""
                },{
                ""wishlistId"": ""W002"",
                ""User"": {""Id"": 2, ""FullName"": ""Hihi""},
                ""Product"": {""Id"": ""P002"", ""ProductName"": ""Road Bike""},
                ""Listing"": {""Id"":""L002"", ""Title"": ""Sell road bike""},
                ""note"": ""Sản phẩm yêu thích"",
                ""createdAt"": ""2024-01-02T00:00:00""
                },{
                ""wishlistId"": ""W003"",
                ""User"": {""Id"": 3, ""FullName"": ""Hehe""},
                ""Product"": {""Id"": ""P003"", ""ProductName"": ""Hybrid Bike""},
                ""Listing"": {""Id"":""L002"", ""Title"": ""Sell Hybrid bike""},
                ""note"": ""Sản phẩm yêu thích"",
                ""createdAt"": ""2024-01-03T00:00:00""
                }
        ]";
            
        }
    }
}
