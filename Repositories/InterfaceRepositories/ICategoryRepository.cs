using Domain.Entities;

namespace Repositories.InterfaceRepositories
{
    public interface ICategoryRepository
    {
        void Add(Mcategory category);
        void Update(Mcategory category);
        Mcategory GetById(string id);
        List<Mcategory> GetAll();
    }
}
