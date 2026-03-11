using Domain.Entities;
using Repositories.InterfaceRepositories;

namespace Repositories.Repositories
{
    public class CategoryRepository :  ICategoryRepository
    {
        private List<Mcategory> _categories = new List<Mcategory>();
        public void Add(Mcategory category)
        {
            _categories.Add(category);
        }

        public void Update(Mcategory category)
        {
            var existing = GetById(category.CategoryId);
            if (existing != null)
            {
                existing.CategoryName = category.CategoryName;
                existing.Description = category.Description;
                existing.IsActive = category.IsActive;
                existing.UpdatedAt = DateTime.Now;
            }
        }

        public Mcategory GetById(string id)
        {
            return _categories.FirstOrDefault(x => x.CategoryId == id);
        }

        public List<Mcategory> GetAll()
        {
            return _categories;
        }
    }
}
