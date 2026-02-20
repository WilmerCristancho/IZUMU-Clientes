using IzumuClientes.API.Middlewares;
using IzumuClientes.Application;
using IzumuClientes.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "IZUMU - Microservicio de Clientes",
        Version = "v1",
        Description = "API para la administración de clientes IZUMU"
    });
});

builder.Services.AddApplication();  
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IZUMU Clientes v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();