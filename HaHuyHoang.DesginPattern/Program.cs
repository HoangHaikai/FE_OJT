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
        IOrderRepository repo = new OrderRepository();
        IOrderAdapter adapter = new OrderAdapter();
        IOrderService service = new OrderService(repo, adapter);

        while (true)
        {
            Console.WriteLine("\n1. Add Order");
            Console.WriteLine("2. Update Order");
            Console.WriteLine("3. Show All");
            Console.WriteLine("0. Exit");

            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var addDto = InputOrder();
                    service.Create(addDto);
                    Console.WriteLine("Added successfully!");
                    break;

                case "2":
                    var updateDto = InputOrder();
                    service.Update(updateDto);
                    Console.WriteLine("Updated successfully!");
                    break;

                case "3":
                    var list = service.GetAll();
                    foreach (var o in list)
                    {
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine($"OrderId: {o.OrderId}");
                        Console.WriteLine($"BuyerId: {o.BuyerId}");
                        Console.WriteLine($"SellerId: {o.SellerId}");
                        Console.WriteLine($"TotalAmount: {o.TotalAmount}");
                        Console.WriteLine($"Receiver: {o.ReceiverName}");
                        Console.WriteLine($"Phone: {o.ReceiverPhone}");
                        Console.WriteLine($"Address: {o.ShippingAddress}");
                        Console.WriteLine($"Note: {o.Note}");
                    }
                    break;

                case "0":
                    return;
            }
        }
    }
    static OrderDTO InputOrder()
    {
        Console.Write("OrderId: ");
        string orderId = Console.ReadLine()!;

        Console.Write("BuyerId: ");
        string buyerId = Console.ReadLine()!;

        Console.Write("SellerId: ");
        string sellerId = Console.ReadLine()!;

        Console.Write("TotalAmount: ");
        decimal totalAmount = decimal.Parse(Console.ReadLine()!);

        Console.Write("Shipping Address: ");
        string address = Console.ReadLine()!;

        Console.Write("Receiver Name: ");
        string receiver = Console.ReadLine()!;

        Console.Write("Receiver Phone: ");
        string phone = Console.ReadLine()!;

        Console.Write("Note: ");
        string note = Console.ReadLine()!;

        return new OrderDTO
        {
            OrderId = orderId,
            BuyerId = buyerId,
            SellerId = sellerId,
            TotalAmount = totalAmount,
            ShippingAddress = address,
            ReceiverName = receiver,
            ReceiverPhone = phone,
            Note = note
        };
    }
}