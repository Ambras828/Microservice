using CategoryService.Domain.Entities;

namespace CategoryService.Domain.Factories
{
    public interface ICategoryFactory
    {
        Category Create(string name, string type, int order);
    }
}
