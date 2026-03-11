using AdapterPattern.Adapter;
using AdapterPattern.IAdapter;
using Domain.DTO;
using Repositories.InterfaceRepositories;
using Repositories.Repositories;
using Services.InterfaceServices;
using Services.Services;

public class Program
{
    public static void Main(string[] args)
    {
        // Brand setup
        IBrandRepository brandRepository = new BrandRepository();
        IBrandAdapter brandAdapter = new BrandAdapter();
        IBrandService brandService = new BrandService(brandRepository, brandAdapter);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== BRAND MANAGEMENT =====");
            Console.WriteLine("1. Create Brand");
            Console.WriteLine("2. Update Brand");
            Console.WriteLine("3. Show All Brands");
            Console.WriteLine("4. Exit");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateBrand(brandService);
                    break;

                case "2":
                    UpdateBrand(brandService);
                    break;

                case "3":
                    ShowAllBrands(brandService);
                    break;

                case "4":
                    exit = true;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    private static void CreateBrand(IBrandService service)
    {
        Console.Write("Enter Brand ID: ");
        string id = Console.ReadLine();

        Console.Write("Enter Brand Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Description: ");
        string desc = Console.ReadLine();

        var dto = new BrandDTO
        {
            BrandId = id,
            BrandName = name,
            Description = desc
        };

        service.CreateBrand(dto);
        Console.WriteLine("Brand created successfully!");
    }

    private static void UpdateBrand(IBrandService service)
    {
        Console.Write("Enter Brand ID to update: ");
        string id = Console.ReadLine();

        Console.Write("Enter New Brand Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Description: ");
        string desc = Console.ReadLine();

        var dto = new BrandDTO
        {
            BrandId = id,
            BrandName = name,
            Description = desc
        };

        service.UpdateBrand(dto);
        Console.WriteLine("Brand updated successfully!");
    }

    private static void ShowAllBrands(IBrandService service)
    {
        var list = service.GetAll();

        if (list.Count == 0)
        {
            Console.WriteLine("No brands found.");
            return;
        }

        Console.WriteLine("\n--- Brand List ---");
        foreach (var b in list)
        {
            Console.WriteLine($"{b.BrandId} - {b.BrandName} - {b.Description}");
        }
    }
}