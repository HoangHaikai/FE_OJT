using AdapterPattern.Adapter;
using AdapterPattern.IAdapter;
using Domain.Data;
using Services.InterfaceServices;
using Services.Services;
using System.Text;
Console.OutputEncoding = Encoding.UTF8;
JsonWishlist jsonWishlist = new JsonWishlist();
IWishlistAdapter wishlistAdapter = new WishlistAdapter(jsonWishlist);
IMWishlistServiceAdapter service = new MWishlistServiceAdapter(wishlistAdapter);

Console.WriteLine("=== GET ALL WISHLISTS ===\n");
foreach (var wishlist in service.GetAllMwishlists())
{
    Console.WriteLine($"Wishlist ID: {wishlist.WishlistId}");
    Console.WriteLine($"User ID: {wishlist.User.FullName}");
    Console.WriteLine($"Product ID: {wishlist.Product.ProductName}");
    Console.WriteLine($"Listing ID: {wishlist.Listing.Title}");
    Console.WriteLine($"Note: {wishlist.Note}");
    Console.WriteLine($"Created At: {wishlist.CreatedAt}");
    Console.WriteLine($"Is Active: {wishlist.DeleteFlag == 0}");
    Console.WriteLine(new string('-', 30));
}