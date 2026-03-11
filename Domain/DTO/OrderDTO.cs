namespace Domain.DTO
{
    public class OrderDTO
    {
        public string OrderId { get; set; }
        public string BuyerId { get; set; }
        public string SellerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public int DeliveryMethod { get; set; }
        public string Note { get; set; }
    }
}
