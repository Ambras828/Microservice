using MediatR;

namespace ProductService.Application.Commands
{
    public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Category,
    string Manufacturer,
    int Quantity,
    decimal Price,
    byte[]? Image
) : IRequest<bool>;

}
