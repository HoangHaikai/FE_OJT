using Domain.Data;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.InterfaceRepositories;
using Repositories.Repositories;
using Services.InterfaceServices;
using Services.Services;
using System;
using System.Text;
Console.OutputEncoding = Encoding.UTF8;
// Tạo ServiceCollection
var services = new ServiceCollection();

// Đăng ký DbContext với PostgreSQL
services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(""));

// Đăng ký Repository
services.AddScoped<IMWishlistRepository, MWishlistRepository>();

// Đăng ký Service
services.AddScoped<IMWishlistService, MWishlistService>();

// Build ServiceProvider
var serviceProvider = services.BuildServiceProvider();

// Lấy service từ DI Container
using var scope = serviceProvider.CreateScope();
var wishlistService = scope.ServiceProvider.GetRequiredService<IMWishlistService>();

Console.WriteLine("=== CREATE NEW WISHLIST ===\n");

// Tạo request data
var request = new CreateWishlistRequest
{
    UserId = "3",
    ProductId = "2",
    ListingId = "3fe5d9c6-62c4-426d-8042-e47e6ecf8a11",
    Note = "I LIKE THIS BIKE!"
};

try
{
    // Gọi service để tạo wishlist
    var result = wishlistService.CreateWishlistAsync(request);

    // Hiển thị kết quả
    Console.WriteLine("CREATE wishlist SUCCESSFULLY!");
    Console.WriteLine($"Wishlist ID: {result.WishlistId}");
    Console.WriteLine($"User ID: {result.UserId}");
    Console.WriteLine($"Product ID: {result.ProductId}");
    Console.WriteLine($"Listing ID: {result.ListingId}");
    Console.WriteLine($"Note: {result.Note}");
    Console.WriteLine($"Created At: {result.CreatedAt}");
    Console.WriteLine($"Is Active: {result.DeleteFlag == 0}");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ BUG: {ex.Message}");
    Console.WriteLine($"DETAILS: {ex.StackTrace}");
}

Console.WriteLine("\nENTER TO EXIT...");
Console.ReadKey();