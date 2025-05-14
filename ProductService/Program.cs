using Infrastructure.Shared.Constants;
using MediatR;
using ProductService.Application.CommandHandlers;
using ProductService.Infrastructure.Interfaces;
using ProductService.Infrastructure.Repositories;
using Infrastructure.Shared.Extensions;
using Infrastructure.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(UpdateProductHandler).Assembly);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.Configure<ConnectionString>(
    builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSharedInfrastructure(builder.Configuration); 

var app = builder.Build();

app.UseHttpsRedirection();
app.UseMiddleware<ListenToOnlyApiGateway>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
