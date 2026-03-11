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
        ICategoryRepository repository = new CategoryRepository();
        ICategoryAdapter adapter = new CategoryAdapte
        r();
        ICategoryService service = new CategoryService(repository, adapter);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== CATEGORY MANAGEMENT =====");
            Console.WriteLine("1. Create Category");
            Console.WriteLine("2. Update Category");
            Console.WriteLine("3. Show All Categories");
            Console.WriteLine("4. Exit");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateCategory(service);
                    break;

                case "2":
                    UpdateCategory(service);
                    break;

                case "3":
                    ShowAll(service);
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

    private static void CreateCategory(ICategoryService service)
    {
        Console.Write("Enter Category ID: ");
        string id = Console.ReadLine();

        Console.Write("Enter Category Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Description: ");
        string desc = Console.ReadLine();

        var dto = new CategoryDTO
        {
            CategoryId = id,
            CategoryName = name,
            Description = desc
        };

        service.CreateCategory(dto);
        Console.WriteLine("Category created successfully!");
    }

    private static void UpdateCategory(ICategoryService service)
    {
        Console.Write("Enter Category ID to update: ");
        string id = Console.ReadLine();

        Console.Write("Enter New Category Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Description: ");
        string desc = Console.ReadLine();

        var dto = new CategoryDTO
        {
            CategoryId = id,
            CategoryName = name,
            Description = desc
        };

        service.UpdateCategory(dto);
        Console.WriteLine("Category updated successfully!");
    }

    private static void ShowAll(ICategoryService service)
    {
        var list = service.GetAll();

        if (list.Count == 0)
        {
            Console.WriteLine("No categories found.");
            return;
        }
Console.WriteLine("\n--- Category List ---");
        foreach (var c in list)
        {
            Console.WriteLine($"{c.CategoryId} - {c.CategoryName} - {c.Description}");
        }
    }
}
