using Domain.DTO;
using Domain.Entities;

namespace Services.InterfaceServices
{
    public interface IOrderService
    {
        void Create(OrderDTO dto);
        void Update(OrderDTO dto);
        List<Morder> GetAll();
    }
}
