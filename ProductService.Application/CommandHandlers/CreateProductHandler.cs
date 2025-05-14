using MediatR;
using ProductService.Application.Commands;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Interfaces;

namespace ProductService.Application.CommandHandlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;

        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Category = request.Category,
                Manufacturer = request.Manufacturer,
                Quantity = request.Quantity,
                Price = request.Price,
                Image = request.Image
            };

            await _repository.CreateAsync(product);
            return Unit.Value;
        }
    }

}
