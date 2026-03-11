using Domain.Entities;

namespace Repositories.InterfaceRepositories
{
    public interface IBrandRepository
    {
        void Add(Mbrand brand);
        void Update(Mbrand brand);
        Mbrand GetById(string id);
        List<Mbrand> GetAll();
    }
}
