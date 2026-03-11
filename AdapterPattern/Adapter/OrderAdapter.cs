using AdapterPattern.IAdapter;
using BuilderPattern.Builder;
using BuilderPattern.InterfaceBuilder;
using Domain.DTO;
using Domain.Entities;

namespace AdapterPattern.Adapter
{
    public class OrderAdapter : IOrderAdapter
    {
        private readonly IOrderBuilder _builder;

        public OrderAdapter()
        {
            _builder = new OrderBuilder();
        }

        public Morder ConvertToEntity(OrderDTO dto)
        {
            return _builder
                .SetOrderId(dto.OrderId)
                .SetBuyerId(dto.BuyerId)
                .SetSellerId(dto.SellerId)
                .SetTotalAmount(dto.TotalAmount)
                .SetShippingAddress(dto.ShippingAddress)
                .SetReceiverName(dto.ReceiverName)
                .SetReceiverPhone(dto.ReceiverPhone)
                .SetDeliveryMethod(dto.DeliveryMethod)
                .SetOrderStatus(0) // Pending
                .SetPaymentStatus(0) // Unpaid
                .SetCreatedAt(DateTime.Now)
                .Build();
        }
    }
}
