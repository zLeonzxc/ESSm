using ESSmAPI.Data;
using ESSmAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<UserContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseInMemoryDatabase("Users");
    //var connString = builder.Configuration.GetConnectionString("DbCon");
    //options.UseSqlServer(connString);
});
builder.Services.AddDbContext<EmployeeContext>(options =>
{
    options.UseInMemoryDatabase("Employees");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.WebHost.UseUrls("https://*:5122");

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
