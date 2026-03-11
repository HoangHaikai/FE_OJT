using Domain.Entities;

namespace BuilderPattern.InterfaceBuilder
{
    public interface IOrderBuilder
    {
        IOrderBuilder SetOrderId(string orderId);
        IOrderBuilder SetBuyerId(string buyerId);
        IOrderBuilder SetSellerId(string sellerId);
        IOrderBuilder SetTotalAmount(decimal totalAmount);
        IOrderBuilder SetShippingAddress(string address);
        IOrderBuilder SetReceiverName(string name);
        IOrderBuilder SetReceiverPhone(string phone);
        IOrderBuilder SetDeliveryMethod(int method);
        IOrderBuilder SetOrderStatus(int status);
        IOrderBuilder SetPaymentStatus(int status);
        IOrderBuilder SetNote(string note);
        IOrderBuilder SetCreatedAt(DateTime date);
        IOrderBuilder SetUpdatedAt(DateTime date);
        IOrderBuilder SetConfirmedAt(DateTime date);
        IOrderBuilder SetCompletedAt(DateTime date);
        IOrderBuilder SetCancelledAt(DateTime date);

        Morder Build();
    }
}
