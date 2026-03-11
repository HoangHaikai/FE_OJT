using Domain.DTO;
using Domain.Entities;

namespace AdapterPattern.IAdapter
{
    public interface IBrandAdapter
    {
        Mbrand ConvertToEntity(BrandDTO dto);
    }
}
