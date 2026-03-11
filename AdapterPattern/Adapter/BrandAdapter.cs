using AdapterPattern.IAdapter;
using BuilderPattern.Builder;
using BuilderPattern.InterfaceBuilder;
using Domain.DTO;
using Domain.Entities;

namespace AdapterPattern.Adapter
{
    public class BrandAdapter : IBrandAdapter
    {
        private readonly IBrandBuilder _builder;

        public BrandAdapter()
        {
            _builder = new BrandBuilder();
        }

        public Mbrand ConvertToEntity(BrandDTO dto)
        {
            return _builder
                .SetId(dto.BrandId)
                .SetName(dto.BrandName)
                .SetDescription(dto.Description)
                .SetIsActive(true)
                .SetCreatedAt(DateTime.Now)
                .Build();
        }
    }
}
