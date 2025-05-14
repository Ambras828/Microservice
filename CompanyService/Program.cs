using Infrastructure.Shared.Middleware;
using Infrastructure.Shared.Constants;
using Infrastructure.Shared.Extensions;
using CompanyService.Infrastructure.Repositories;

using CompanyService.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<ConnectionString>(
    builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ListenToOnlyApiGateway>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();