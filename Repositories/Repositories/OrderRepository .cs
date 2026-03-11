using Domain.Entities;
using Repositories.InterfaceRepositories;

namespace Repositories.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Morder> _orders = new();

        public void Add(Morder order)
        {
            _orders.Add(order);
        }

        public void Update(Morder order)
        {
            var existing = _orders.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (existing != null)
            {
                existing.BuyerId = order.BuyerId;
                existing.SellerId = order.SellerId;
                existing.TotalAmount = order.TotalAmount;
                existing.ShippingAddress = order.ShippingAddress;
                existing.ReceiverName = order.ReceiverName;
                existing.ReceiverPhone = order.ReceiverPhone;
                existing.Note = order.Note;
                existing.UpdatedAt = DateTime.Now;
            }
        }

        public List<Morder> GetAll()
        {
            return _orders;
        }

        public Morder? GetById(string id)
        {
            return _orders.FirstOrDefault(x => x.OrderId == id);
        }
    }
}
