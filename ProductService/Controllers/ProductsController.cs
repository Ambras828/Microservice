using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Commands;
using ProductService.Application.Queries;
using ProductService.Domain.Entities;
using Infrastructure.Shared.Response;

namespace ProductService.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(ApiResponse<IEnumerable<Product>>.SuccessResponse(products, "Products retrieved successfully"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
            return NotFound(ApiResponse<string>.ErrorResponse("Product not found"));

        return Ok(ApiResponse<Product>.SuccessResponse(product, "Product retrieved successfully"));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        var command = new CreateProductCommand(
            product.Id,
            product.Name,
            product.Category,
            product.Manufacturer,
            product.Quantity,
            product.Price,
            product.Image
        );

        await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = product.Id },
            ApiResponse<Product>.SuccessResponse(product, "Product created successfully"));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest(ApiResponse<string>.ErrorResponse("Mismatched product ID"));

        var command = new UpdateProductCommand(
            product.Id,
            product.Name,
            product.Category,
            product.Manufacturer,
            product.Quantity,
            product.Price,
            product.Image
        );

        var success = await _mediator.Send(command);
        if (!success)
            return NotFound(ApiResponse<string>.ErrorResponse("Product not found"));

        return Ok(ApiResponse<string>.SuccessResponse("Product updated successfully"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteProductCommand(id));
        if (!success)
            return NotFound(ApiResponse<string>.ErrorResponse("Product not found"));

        return Ok(ApiResponse<string>.SuccessResponse("Product deleted successfully"));
    }
}
