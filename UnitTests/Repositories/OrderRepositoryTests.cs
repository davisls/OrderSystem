using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Repositories;

public class OrderRepositoryTests
{
    [Fact]
    public async Task AddAsync_Should_Save_Order()
    {
        var options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

        await using var context =
            new AppDbContext(options);

        var repository =
            new OrderRepository(context);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Customer = "Davi",
            Value = 100
        };

        await repository.AddAsync(order);

        var savedOrder =
            await context.Orders.FirstOrDefaultAsync();

        savedOrder.Should().NotBeNull();

        savedOrder!.Customer
            .Should()
            .Be("Davi");
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Orders()
    {
        var options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString())
                .Options;

        await using var context =
            new AppDbContext(options);

        context.Orders.AddRange(
            new Order
            {
                Customer = "Order 1",
                Value = 100
            },
            new Order
            {
                Customer = "Order 2",
                Value = 200
            });

        await context.SaveChangesAsync();

        var repository =
            new OrderRepository(context);

        var result =
            await repository.GetAllAsync();

        result.Should().HaveCount(2);
    }
}