using Domain.Entities;

namespace Infrastructure.Services;

public interface IMongoCacheService
{
    Task<Order?> GetAsync(Guid id);

    Task SaveAsync(Order order);
}