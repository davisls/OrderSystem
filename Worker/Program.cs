using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        "Server=localhost,1433;Database=OrdersDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True");
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHostedService<OrderConsumerService>();

var host = builder.Build();

host.Run();