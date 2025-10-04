using ApiTask.Data;
using ApiTask.Interface;
using ApiTask.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString(nameof(MainDbContext));

if (connection == null)
{
    throw new ArgumentException(nameof(connection));
}

builder.Services.AddDbContext<MainDbContext>(opt => opt.UseSqlServer(connection));

builder.Services.AddScoped<ICombinationService, CombinationService>();
builder.Services.AddSingleton<ICombinationGenerator, CombinationGenerationService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
