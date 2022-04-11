using System.Text;
using Discord.Commands;
using LordOfFrames.Database;

namespace LordOfFrames.App.Commands;

public class ListGamesCommand : CommandBase
{
    private GameRepository _repository;

    public ListGamesCommand(GameRepository repository)
    {
        _repository = repository;
    }
    
    [Command("listgames")]
    public async Task HandleCommandAsync()
    {
        var sb = new StringBuilder();
        var games = await _repository.GetAllGames();
        foreach (var game in games.OrderBy(g => g.Name))
        {
            sb.Append($"- {game.Name} (id: {game.Id})\n");
        }
        var dmChannel = await GuildUser.CreateDMChannelAsync();
        await dmChannel.SendMessageAsync($"Game List:\n{sb}");
    }
}