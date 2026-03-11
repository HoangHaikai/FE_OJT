using Domain.Entities;

namespace Repositories.InterfaceRepositories
{
    public interface IOrderRepository
    {
        void Add(Morder order);
        void Update(Morder order);
        List<Morder> GetAll();
        Morder? GetById(string id);
    }
}
