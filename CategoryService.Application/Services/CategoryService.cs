using AutoMapper;
using CategoryService.Application.DTOs;
using CategoryService.Application.Interfaces;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Factories;
using CategoryService.Infrastructure.Interfaces;

namespace CategoryService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryFactory _factory;

        public CategoryService(ICategoryRepository repository, IMapper mapper, ICategoryFactory factory)
        {
            _repository = repository;
            _mapper = mapper;
            _factory = factory;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<Guid> CreateAsync(CreateCategoryDto dto)
        {
            var category = _factory.Create(dto.CategoryName, dto.CategoryType, dto.Orders);
            await _repository.CreateAsync(category);
            return category.Id;
        }

        public async Task<bool> UpdateAsync(UpdateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            return await _repository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
