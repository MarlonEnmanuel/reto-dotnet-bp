using ClientsApi;
using ClientsApi.Infrastructure.Controllers;
using ClientsApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DB Context with SQL Server
builder.Services.AddDbContext<ClientsContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ClientsDb") ??
        throw new InvalidOperationException("Connection string 'ClientsDb' not found");
    options.UseSqlServer(connectionString);
});

// Configure app dependencies
builder.Services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configure global exception handler
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure app routes
app.MapClientRoutes();

app.Run();
