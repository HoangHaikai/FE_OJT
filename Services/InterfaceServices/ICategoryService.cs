using Domain.DTO;
using Domain.Entities;

namespace Services.InterfaceServices
{
    public interface ICategoryService
    {
        void CreateCategory(CategoryDTO dto);
        void UpdateCategory(CategoryDTO dto);
        List<Mcategory> GetAll();
    }
}
