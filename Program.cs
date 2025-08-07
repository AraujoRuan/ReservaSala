using Microsoft.EntityFrameworkCore;
using ReservaSalaAPI.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReservaContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ReservaConnection"),
        new MySqlServerVersion(new Version(9, 0, 0))
    ));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();
app.MapControllers();
app.Run();