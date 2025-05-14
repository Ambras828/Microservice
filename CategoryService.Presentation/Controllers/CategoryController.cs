using Microsoft.AspNetCore.Mvc;
using CategoryService.Application.DTOs;
using CategoryService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Shared.Response;

namespace CategoryService.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();
        if (categories == null || !categories.Any())
            return NotFound(ApiResponse<string>.ErrorResponse("No categories found"));

        return Ok(ApiResponse<IEnumerable<CategoryDto>>.SuccessResponse(categories, "Categories retrieved successfully"));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category == null)
            return NotFound(ApiResponse<string>.ErrorResponse("Category not found"));

        return Ok(ApiResponse<CategoryDto>.SuccessResponse(category, "Category retrieved successfully"));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, ApiResponse<Guid>.SuccessResponse(id, "Category created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        if (id != dto.Id)
            return BadRequest(ApiResponse<string>.ErrorResponse("Mismatched ID"));

        var result = await _service.UpdateAsync(dto);
        if (!result)
            return NotFound(ApiResponse<string>.ErrorResponse("Category not found"));

        return Ok(ApiResponse<string>.SuccessResponse("Category updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
            return NotFound(ApiResponse<string>.ErrorResponse("Category not found"));

        return Ok(ApiResponse<string>.SuccessResponse("Category deleted successfully"));
    }
}
