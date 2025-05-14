using CategoryService.Domain.Entities;
using Dapper;
using CategoryService.Infrastructure.Interfaces;
using Infrastructure.Shared.Database;
using System.Data;
using DataAccess.Repository;

namespace CategoryService.Infrastructure.Repository
{

    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {

        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await SelectAsync<Category>("sp_GetAllCategories");
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await SelectFirstOrDefaultAsync<Category>("sp_GetCategoryById", new { Id = id });
        }

        public async Task CreateAsync(Category category)
        {
            await ExecuteAsync(
                "sp_CreateCategory",
                new
                {
                    category.Id,
                    category.CategoryName,
                    category.CategoryType,
                    category.Orders
                });
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            var result = await ExecuteAsync(
                "sp_UpdateCategory",
                new
                {
                    category.Id,
                    category.CategoryName,
                    category.CategoryType,
                    category.Orders
                });

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await ExecuteAsync(
                "sp_DeleteCategory", new { Id = id });

            return result > 0;
        }
    }

}