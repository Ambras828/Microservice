using CategoryService.Domain.Entities;

namespace CategoryService.Domain.Factories
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category Create(string name, string type, int order)
        {
            //  could inject more logic here for different types
            return new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = name,
                CategoryType = type,
                Orders = order
            };
        }
    }
}
