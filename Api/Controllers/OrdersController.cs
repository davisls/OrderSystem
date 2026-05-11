using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Messaging;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly RabbitMqPublisher _publisher;
    private readonly IOrderRepository _repository; 
    private readonly IMongoCacheService _cache;
    private readonly ILogger<OrdersController> _logger;
    public OrdersController(
        RabbitMqPublisher publisher,
        IOrderRepository repository,
        IMongoCacheService cache,
        ILogger<OrdersController> logger)
    {
        _publisher = publisher;
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        _logger.LogInformation("Creating order for customer {Customer}", dto.Customer);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Customer = dto.Customer,
            Value = dto.Value,
            OrderDate = dto.OrderDate
        };

        await _publisher.PublishAsync(order);

        return Accepted(order);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        _logger.LogInformation("Fetching order {OrderId}", id);

        var cachedOrder =
            await _cache.GetAsync(id);

        if (cachedOrder is not null)
        {
            _logger.LogInformation("CACHE HIT for order {OrderId}", id);

            return Ok(cachedOrder);
        }

        _logger.LogInformation("CACHE MISS for order {OrderId}", id);

        var order =
            await _repository.GetByIdAsync(id);

        if (order is null)
            return NotFound();

        await _cache.SaveAsync(order);

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Fetching all orders");

        var orders = await _repository.GetAllAsync();

        return Ok(orders);
    }
}