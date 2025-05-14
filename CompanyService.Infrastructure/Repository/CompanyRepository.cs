using CompanyService.Application.Interfaces;
using CompanyService.Domain.Entities;
using Infrastructure.Shared.Database;
using DataAccess.Repository;

namespace CompanyService.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await SelectAsync<Company>("sp_GetAllCompanies");
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            return await SelectFirstOrDefaultAsync<Company>("sp_GetCompanyById", new { Id = id });
        }

        public async Task CreateAsync(Company company)
        {
            await ExecuteAsync("sp_CreateCompany", company);
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            var result = await ExecuteAsync("sp_UpdateCompany", company);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await ExecuteAsync("sp_DeleteCompany", new { Id = id });
            return result > 0;
        }
    }
}
