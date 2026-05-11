using Api.Controllers;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Infrastructure.Messaging;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Controllers;

public class OrdersControllerTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Mock<ILogger<OrdersController>> _loggerMock;
    private readonly Mock<IMongoCacheService> _cacheMock;
    private readonly OrdersController _controller;

    public OrdersControllerTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();

        _loggerMock =
            new Mock<ILogger<OrdersController>>();

        _cacheMock =
            new Mock<IMongoCacheService>();

        var publisher = new RabbitMqPublisher();



        _controller = new OrdersController(
            publisher,
            _repositoryMock.Object,
            _cacheMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task GetById_Should_Return_Order_When_Order_Exists()
    {
        var orderId = Guid.NewGuid();

        var order = new Order
        {
            Id = orderId,
            Customer = "Davi",
            Value = 150
        };

        _cacheMock
            .Setup(x => x.GetAsync(orderId))
            .ReturnsAsync((Order?)null);

        _repositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        var result =
            await _controller.Get(orderId);

        result.Should()
            .BeOfType<OkObjectResult>();

        var okResult =
            result as OkObjectResult;

        okResult!.Value
            .Should()
            .BeEquivalentTo(order);
    }

    [Fact]
    public async Task GetById_Should_Return_NotFound_When_Order_Does_Not_Exist()
    {
        var orderId = Guid.NewGuid();

        _cacheMock
            .Setup(x => x.GetAsync(orderId))
            .ReturnsAsync((Order?)null);

        _repositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync((Order?)null);

        var result =
            await _controller.Get(orderId);

        result.Should()
            .BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_Should_Return_Accepted()
    {
        var dto = new CreateOrderDto
        {
            Customer = "Davi",
            Value = 200,
            OrderDate = DateTime.UtcNow
        };

        var result = await _controller.Create(dto);

        result.Should().BeOfType<AcceptedResult>();
    }
}

