using clinic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using MySql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using clinic.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClinicContext>(options =>
{
    string connectionString = "Server=202.182.120.224;Database=clinic;User ID=remote4;Password=$C3u+X[Nm-gg6E!j;Connection Timeout=100";
    //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.UseMySQL(connectionString);
});

builder.Services.AddScoped<ClinicService>();

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
