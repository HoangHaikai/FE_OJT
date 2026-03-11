
using AdapterPattern.IAdapter;
using Domain.DTO;
using Domain.Entities;
using Repositories.InterfaceRepositories;
using Services.InterfaceServices;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryAdapter _adapter;

        public CategoryService(ICategoryRepository repository, ICategoryAdapter adapter)
        {
            _repository = repository;
            _adapter = adapter;
        }

        public void CreateCategory(CategoryDTO dto)
        {
            var entity = _adapter.ConvertToEntity(dto);
            _repository.Add(entity);
        }

        public void UpdateCategory(CategoryDTO dto)
        {
            var existing = _repository.GetById(dto.CategoryId);
            if (existing == null)
            {
                Console.WriteLine("Category not found!");
                return;
            }

            existing.CategoryName = dto.CategoryName;
            existing.Description = dto.Description;
            existing.UpdatedAt = DateTime.Now;

            _repository.Update(existing);
        }

        public List<Mcategory> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
