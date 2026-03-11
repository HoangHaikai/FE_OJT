using Domain.Entities;

namespace BuilderPattern.InterfaceBuilder
{
    public interface ICategoryBuilder
    {
        ICategoryBuilder SetId(string id);
        ICategoryBuilder SetName(string name);
        ICategoryBuilder SetDescription(string description);
        ICategoryBuilder SetIsActive(bool isActive);
        ICategoryBuilder SetCreatedAt(DateTime date);
        ICategoryBuilder SetUpdatedAt(DateTime date);
        Mcategory Build();

    }
}
