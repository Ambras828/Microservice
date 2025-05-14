
using MediatR;
using ProductService.Application.Commands;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Interfaces;

namespace ProductService.Application.CommandHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public UpdateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

            return await _repository.UpdateAsync(product);
        }
    }

}
