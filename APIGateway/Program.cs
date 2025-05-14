using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Infrastructure.Shared.Authentication;
using APIGateway.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddJWTAuthenticatioSchema(builder.Configuration);

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseMiddleware<AttachSignatureToRequest>();
app.UseAuthentication(); 
app.UseAuthorization();

await app.UseOcelot();
app.Run();
