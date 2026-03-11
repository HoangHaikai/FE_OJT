using BuilderPattern.InterfaceBuilder;
using Domain.Entities;

namespace BuilderPattern.Builder
{
    public class BrandBuilder : IBrandBuilder
    {
        private Mbrand _brand;

        public BrandBuilder()
        {
            _brand = new Mbrand();
        }

        public IBrandBuilder SetId(string id)
        {
            _brand.BrandId = id;
            return this;
        }

        public IBrandBuilder SetName(string name)
        {
            _brand.BrandName = name;
            return this;
        }

        public IBrandBuilder SetDescription(string description)
        {
            _brand.Description = description;
            return this;
        }

        public IBrandBuilder SetIsActive(bool isActive)
        {
            _brand.IsActive = isActive;
            return this;
        }

        public IBrandBuilder SetCreatedAt(DateTime date)
        {
            _brand.CreatedAt = date;
            return this;
        }

        public IBrandBuilder SetUpdatedAt(DateTime date)
        {
            _brand.UpdatedAt = date;
            return this;
        }

        public Mbrand Build()
        {
            return _brand;
        }
    }
}
