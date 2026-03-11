using BuilderPattern.InterfaceBuilder;
using Domain.Entities;

namespace BuilderPattern.Builder
{
    public class CategoryBuilder : ICategoryBuilder
    {
        private Mcategory _category;

        public CategoryBuilder()
        {
            _category = new Mcategory();
        }

        public ICategoryBuilder SetId(string id)
        {
            _category.CategoryId = id;
            return this;
        }

        public ICategoryBuilder SetName(string name)
        {
            _category.CategoryName = name;
            return this;
        }

        public ICategoryBuilder SetDescription(string description)
        {
            _category.Description = description;
            return this;
        }

        public ICategoryBuilder SetIsActive(bool isActive)
        {
            _category.IsActive = isActive;
            return this;
        }

        public ICategoryBuilder SetCreatedAt(DateTime date)
        {
            _category.CreatedAt = date;
            return this;
        }

        public ICategoryBuilder SetUpdatedAt(DateTime date)
        {
            _category.UpdatedAt = date;
            return this;
        }

        public Mcategory Build()
        {
            return _category;
        }
    }
}
