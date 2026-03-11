using AdapterPattern.IAdapter;
using BuilderPattern.Builder;
using BuilderPattern.InterfaceBuilder;
using Domain.DTO;
using Domain.Entities;

namespace AdapterPattern.Adapter
{
    public class CategoryAdapter : ICategoryAdapter
    {
        private readonly ICategoryBuilder _builder;

        public CategoryAdapter()
        {
            _builder = new CategoryBuilder();
        }

        public Mcategory ConvertToEntity(CategoryDTO dto)
        {
            return _builder
                .SetId(dto.CategoryId)
                .SetName(dto.CategoryName)
                .SetDescription(dto.Description)
                .SetIsActive(true)
                .SetCreatedAt(DateTime.Now)
                .Build();
        }
    }
}