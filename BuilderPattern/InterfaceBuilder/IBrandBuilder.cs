using Domain.Entities;

namespace BuilderPattern.InterfaceBuilder
{
    public interface IBrandBuilder
    {
        IBrandBuilder SetId(string id);
        IBrandBuilder SetName(string name);
        IBrandBuilder SetDescription(string description);
        IBrandBuilder SetIsActive(bool isActive);
        IBrandBuilder SetCreatedAt(DateTime date);
        IBrandBuilder SetUpdatedAt(DateTime date);
        Mbrand Build();

    }
}
