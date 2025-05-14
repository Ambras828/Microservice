using AuthenticationService.Services;
using Infrastructure.Shared.Extensions;
using Infrastructure.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSharedInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ListenToOnlyApiGateway>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
