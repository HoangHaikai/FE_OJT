using Domain.Entities;
using Repositories.InterfaceRepositories;

namespace Repositories.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private List<Mbrand> _brands = new List<Mbrand>();
        public void Add(Mbrand brand)
        {
            _brands.Add(brand);
        }

        public void Update(Mbrand brand)
        {
            var existing = GetById(brand.BrandId);
            if (existing != null)
            {
                existing.BrandName = brand.BrandName;
                existing.Description = brand.Description;
                existing.IsActive = brand.IsActive;
                existing.UpdatedAt = DateTime.Now;
            }
        }

        public Mbrand GetById(string id)
        {
            return _brands.FirstOrDefault(x => x.BrandId == id);
        }

        public List<Mbrand> GetAll()
        {
            return _brands;
        }
    }
}
