using LordOfFrames.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LordOfFrames.Database;

public class GameRepository
{
    private readonly IMongoClient _mongoClient;

    public GameRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<IEnumerable<Game>> GetAll()
    {
        List<Game> gameCollection = await GetCollection().AsQueryable().ToListAsync();
        return gameCollection;
    }

    public async Task<Game> GetById(string id)
    {
        var game = await GetCollection().AsQueryable()
            .FirstOrDefaultAsync(g => g.Id == id);
        return game;
    }

    public async Task<Game> Create(Game data)
    {
        data.CreatedAt = DateTime.UtcNow;
        data.UpdatedAt = data.CreatedAt;
        await GetCollection().InsertOneAsync(data);
        var gameList = await GetCollection().AsQueryable().ToListAsync();
        return gameList.FirstOrDefault(x => x.Id == data.Id);
    }

    public async Task<Game> Update(string id, Game data)
    {
        data.UpdatedAt = DateTime.UtcNow;
        data.Version++;
        await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
        return data;
    }

    public async Task Delete(string id)
    {
        await GetCollection().DeleteOneAsync(x => x.Id == id);
    }
    
    private IMongoCollection<Game> GetCollection()
    {
        IMongoDatabase database = _mongoClient.GetDatabase("lordofframes");
        IMongoCollection<Game> collection = database.GetCollection<Game>("games");
        return collection;
    }
}