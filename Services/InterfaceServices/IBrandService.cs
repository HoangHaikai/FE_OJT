using Domain.DTO;
using Domain.Entities;

namespace Services.InterfaceServices
{
    public interface IBrandService
    {
        void CreateBrand(BrandDTO dto);
        void UpdateBrand(BrandDTO dto);
        List<Mbrand> GetAll();
    }
}
