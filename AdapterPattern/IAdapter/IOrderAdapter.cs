using Domain.DTO;
using Domain.Entities;

namespace AdapterPattern.IAdapter
{
    public interface IOrderAdapter
    {
        Morder ConvertToEntity(OrderDTO dto);
    }
}
