using Domain.DTO;
using Domain.Entities;

namespace AdapterPattern.IAdapter
{
    public interface ICategoryAdapter
    {
        Mcategory ConvertToEntity(CategoryDTO dto);
    }
}
