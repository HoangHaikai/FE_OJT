using AdapterPattern.IAdapter;
using Domain.DTO;
using Domain.Entities;
using Repositories.InterfaceRepositories;
using Services.InterfaceServices;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderAdapter _adapter;

        public OrderService(IOrderRepository repo, IOrderAdapter adapter)
        {
            _repository = repo;
            _adapter = adapter;
        }
        public void Update(OrderDTO dto)
        {
            var entity = _adapter.ConvertToEntity(dto);
            entity.UpdatedAt = DateTime.Now;
            _repository.Update(entity);
        }

        public List<Morder> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(OrderDTO dto)
        {
            var entity = _adapter.ConvertToEntity(dto);
            _repository.Add(entity);
        }

    }
}
