using CategoryService.Application.Interfaces;
using CategoryService.Application.Mapping;
using Infrastructure.Shared.Extensions;
using CategoryService.Infrastructure.Repository;
using CategoryService.Domain.Factories;
using CategoryService.Infrastructure.Interfaces;
using Infrastructure.Shared.Middleware;
using Infrastructure.Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConnectionString>(
    builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(CategoryProfile));

builder.Services.AddScoped<ICategoryFactory, CategoryFactory>();
builder.Services.AddScoped<ICategoryService, CategoryService.Application.Services.CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ListenToOnlyApiGateway>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
