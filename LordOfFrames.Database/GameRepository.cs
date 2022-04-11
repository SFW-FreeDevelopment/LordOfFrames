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

    public async Task<IEnumerable<Game>> GetAllGames()
    {
        List<Game> gameCollection = await GetCollection().AsQueryable().ToListAsync();
        return gameCollection.OrderBy(g => g.Name);
    }

    public async Task<Game> GetGameById(string id)
    {
        var game = await GetCollection().AsQueryable()
            .FirstOrDefaultAsync(g => g.Id == id);
        return game;
    }

    public async Task<Game> CreateGame(Game data)
    {
        data.Version = 1;
        data.CreatedAt = DateTime.UtcNow;
        data.UpdatedAt = data.CreatedAt;
        await GetCollection().InsertOneAsync(data);
        var gameList = await GetCollection().AsQueryable().ToListAsync();
        return gameList.FirstOrDefault(x => x.Id == data.Id);
    }

    public async Task<Game> UpdateGame(string id, Game data)
    {
        data.UpdatedAt = DateTime.UtcNow;
        data.Version++;
        await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
        return data;
    }
    public async Task<IEnumerable<Character>> GetAllCharactersByGameId(string id)
    {
        var game = await GetGameById(id);
        return game.Characters.OrderBy(c => c.Name);
    }
    
    public async Task<Character> GetCharacterByGameIdAndSlug(string id, string slug)
    {
        var game = await GetGameById(id);
        return game.Characters.FirstOrDefault(c=> string.Equals(c.Slug,slug, StringComparison.OrdinalIgnoreCase));
    }
    
    public async Task<Character> CreateCharacter(string id, Character data)
    {
        var game = await GetGameById(id);
        game.Characters.Add(data);
        game.UpdatedAt = DateTime.UtcNow;
        game.Version++;
        await GetCollection().ReplaceOneAsync(x => x.Id == id, game);
        return data;
    }
  
    public async Task<Character> UpdateCharacter(string id, Character data)
    {
        var game = await GetGameById(id);
        var character = game.Characters.FirstOrDefault(c=> string.Equals(c.Slug,data.Slug, StringComparison.OrdinalIgnoreCase));
        data.Slug = character?.Slug;
        data.Name = character?.Name;
        character?.Map(data);
        game.UpdatedAt = DateTime.UtcNow;
        game.Version++;
        await GetCollection().ReplaceOneAsync(x => x.Id == id, game);
        return data;
    }
    //TODO: get all system mechanic by game
    
    //TODO: get system mechanic by game and slug
    
    //TODO: create system mechanic 
    
    //TODO: update system mechanic 
    
    
    private IMongoCollection<Game> GetCollection()
    {
        IMongoDatabase database = _mongoClient.GetDatabase("lordofframes");
        IMongoCollection<Game> collection = database.GetCollection<Game>("games");
        return collection;
    }
}