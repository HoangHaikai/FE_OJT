
using AdapterPattern.IAdapter;
using Domain.DTO;
using Domain.Entities;
using Repositories.InterfaceRepositories;
using Services.InterfaceServices;

namespace Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repository;
        private readonly IBrandAdapter _adapter;

        public BrandService(IBrandRepository repository, IBrandAdapter adapter)
        {
            _repository = repository;
            _adapter = adapter;
        }

        public void CreateBrand(BrandDTO dto)
        {
            var entity = _adapter.ConvertToEntity(dto);
            _repository.Add(entity);
        }

        public void UpdateBrand(BrandDTO dto)
        {
            var existing = _repository.GetById(dto.BrandId);
            if (existing == null)
            {
                Console.WriteLine("Brand not found!");
                return;
            }

            existing.BrandName = dto.BrandName;
            existing.Description = dto.Description;
            existing.UpdatedAt = DateTime.Now;

            _repository.Update(existing);
        }

        public List<Mbrand> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
