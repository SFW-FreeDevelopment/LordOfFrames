using LordOfFrames.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LordOfFrames.Api.Repositories;

public class ApiKeyRepository
{
    private readonly IMongoClient _mongoClient;

    public ApiKeyRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }
    
    public async Task<bool> ValidateKey(string key) 
    {
        var apiKey = await GetCollection().AsQueryable()
            .FirstOrDefaultAsync(a => a.Key.Equals(key));
        return apiKey != null;
    }
    
    private IMongoCollection<ApiKey> GetCollection()
    {
        IMongoDatabase database = _mongoClient.GetDatabase("lordofframes");
        IMongoCollection<ApiKey> collection = database.GetCollection<ApiKey>("apikeys");
        return collection;
    }
}