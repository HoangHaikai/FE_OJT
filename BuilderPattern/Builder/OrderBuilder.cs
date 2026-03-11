using BuilderPattern.InterfaceBuilder;
using Domain.Entities;

namespace BuilderPattern.Builder
{
    public class OrderBuilder : IOrderBuilder
    {
        private Morder _order;

        public OrderBuilder()
        {
            _order = new Morder();
        }

        public IOrderBuilder SetOrderId(string orderId)
        {
            _order.OrderId = orderId;
            return this;
        }

        public IOrderBuilder SetBuyerId(string buyerId)
        {
            _order.BuyerId = buyerId;
            return this;
        }

        public IOrderBuilder SetSellerId(string sellerId)
        {
            _order.SellerId = sellerId;
            return this;
        }

        public IOrderBuilder SetTotalAmount(decimal totalAmount)
        {
            _order.TotalAmount = totalAmount;
            return this;
        }

        public IOrderBuilder SetShippingAddress(string address)
        {
            _order.ShippingAddress = address;
            return this;
        }

        public IOrderBuilder SetReceiverName(string name)
        {
            _order.ReceiverName = name;
            return this;
        }

        public IOrderBuilder SetReceiverPhone(string phone)
        {
            _order.ReceiverPhone = phone;
            return this;
        }

        public IOrderBuilder SetDeliveryMethod(int method)
        {
            _order.DeliveryMethod = method;
            return this;
        }

        public IOrderBuilder SetOrderStatus(int status)
        {
            _order.OrderStatus = status;
            return this;
        }

        public IOrderBuilder SetPaymentStatus(int status)
        {
            _order.PaymentStatus = status;
            return this;
        }

        public IOrderBuilder SetNote(string note)
        {
            _order.Note = note;
            return this;
        }

        public IOrderBuilder SetCreatedAt(DateTime date)
        {
            _order.CreatedAt = date;
            return this;
        }

        public IOrderBuilder SetUpdatedAt(DateTime date)
        {
            _order.UpdatedAt = date;
            return this;
        }

        public IOrderBuilder SetConfirmedAt(DateTime date)
        {
            _order.ConfirmedAt = date;
            return this;
        }

        public IOrderBuilder SetCompletedAt(DateTime date)
        {
            _order.CompletedAt = date;
            return this;
        }

        public IOrderBuilder SetCancelledAt(DateTime date)
        {
            _order.CancelledAt = date;
            return this;
        }

        public Morder Build()
        {
            var result = _order;
            _order = new Morder(); // reset tránh reuse
            return result;
        }

    }
}
