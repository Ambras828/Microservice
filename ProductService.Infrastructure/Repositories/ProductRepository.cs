using ProductService.Domain.Entities;
using ProductService.Infrastructure.Interfaces;
using DataAccess.Repository;
using Infrastructure.Shared.Database;


namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await SelectAsync<Product>("sp_GetAllProducts");
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await SelectFirstOrDefaultAsync<Product>("sp_GetProductById", new { Id = id });
        }

        public async Task CreateAsync(Product product)
        {
            await ExecuteAsync(
                "sp_CreateProduct",
                new
                {
                    product.Id,
                    product.Name,
                    product.Category,
                    product.Manufacturer,
                    product.Quantity,
                    product.Price,
                    product.Image
                });
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var result = await ExecuteAsync(
                "sp_UpdateProduct",
                new
                {
                    product.Id,
                    product.Name,
                    product.Category,
                    product.Manufacturer,
                    product.Quantity,
                    product.Price,
                    product.Image
                });

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await ExecuteAsync("sp_DeleteProduct", new { Id = id });
            return result > 0;
        }
    }

}
