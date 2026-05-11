using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class MongoCacheService
    : IMongoCacheService
{
    private readonly IMongoCollection<Order> _collection;

    public MongoCacheService(
        IOptions<MongoSettings> settings)
    {
        var mongoClient = new MongoClient(
            settings.Value.ConnectionString);

        var database = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        _collection = database.GetCollection<Order>(
            settings.Value.OrdersCollection);
    }

    public async Task<Order?> GetAsync(Guid id)
    {
        return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task SaveAsync(Order order)
    {
        await _collection.ReplaceOneAsync(
            x => x.Id == order.Id,
            order,
            new ReplaceOptions
            {
                IsUpsert = true
            });
    }
}