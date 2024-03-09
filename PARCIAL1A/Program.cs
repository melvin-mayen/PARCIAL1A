using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// iNYECCION POR DEPENDENCIA DEL STRING DE CONEXION AL CONTEXTO
builder.Services.AddDbContext<autoresContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("autoresDbConnection")
    )
  );




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
