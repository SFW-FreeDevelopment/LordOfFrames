using System.Text;
using Discord.Commands;
using LordOfFrames.Database;

namespace LordOfFrames.App.Commands;

public class FullCharacterCommand : CommandBase
{
    private GameRepository _repository;

    public FullCharacterCommand(GameRepository repository)
    {
        _repository = repository;
    }

    [Command("fullcharacter")]
    public async Task HandleCommandAsync([Remainder] string remainder)
    {
        var split = remainder.Split(" ");
        if (split.Length == 2)
        {
            var moveText = new StringBuilder();
            var character = await _repository.GetCharacterByGameIdAndSlug(split[0], split[1]);
            
            foreach (var move in character.Moves)
            {
                moveText.Append($"- **{move.Name}** (id: {move.Slug})\n" +
                                $"Type: {move.MapMoveTypeEnumToDisplayString()}\n" +
                                $"Input: {move.Input}\n" +
                                $"Startup: {move.Startup}\n" +
                                $"Active: {move.Active}\n" +
                                $"OnHit: {move.OnHit}\n" +
                                $"OnBlock: {move.OnBlock}\n" +
                                $"Recovery: {move.Recovery}\n\n");
            }

            var characterText = $"**{character.Name}** (id: {character.Slug})\n" +
                                $"Description: {character.Description}\n\n" +
                                "**Moves**:\n" +
                                $"{moveText}";
            var dmChannel = await GuildUser.CreateDMChannelAsync();
            await dmChannel.SendMessageAsync(characterText);
        }
    }
}