using System.Text;
using Discord.Commands;
using LordOfFrames.Database;

namespace LordOfFrames.App.Commands;

public class ListCharactersCommand : CommandBase
{
    private GameRepository _repository;

    public ListCharactersCommand(GameRepository repository)
    {
        _repository = repository;
    }

    [Command("listcharacters")]
    public async Task HandleCommandAsync([Remainder] string gameId)
    {
        var sb = new StringBuilder();
        var characters = await _repository.GetAllCharactersByGameId(gameId);
        foreach (var character in characters.OrderBy(g => g.Name))
        {
            sb.Append($"- **{character.Name}** (id: {character.Slug})\n");
        }
        var dmChannel = await GuildUser.CreateDMChannelAsync();
        await dmChannel.SendMessageAsync($"**Character List**:\n{sb}");
    }
}