using CompanyService.Application.Interfaces;
using CompanyService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Shared.Response;

namespace CompanyService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repository;

        public CompanyController(ICompanyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _repository.GetAllAsync();
            if (companies == null || !companies.Any())
                return NotFound(ApiResponse<string>.ErrorResponse("No companies found"));

            return Ok(ApiResponse<IEnumerable<Company>>.SuccessResponse(companies, "Companies retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _repository.GetByIdAsync(id);
            if (company == null)
                return NotFound(ApiResponse<string>.ErrorResponse("Company not found"));

            return Ok(ApiResponse<Company>.SuccessResponse(company, "Company retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            await _repository.CreateAsync(company);
            return CreatedAtAction(nameof(GetById), new { id = company.Id },
                ApiResponse<Company>.SuccessResponse(company, "Company created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Company company)
        {
            if (id != company.Id)
                return BadRequest(ApiResponse<string>.ErrorResponse("Mismatched company ID"));

            var updated = await _repository.UpdateAsync(company);
            if (!updated)
                return NotFound(ApiResponse<string>.ErrorResponse("Company not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Company updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.ErrorResponse("Company not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Company deleted successfully"));
        }
    }
}
