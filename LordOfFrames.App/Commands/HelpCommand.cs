using Discord.Commands;

namespace LordOfFrames.App.Commands;

public class HelpCommand : CommandBase
{
    [Command("help")]
    public async Task HandleCommandAsync()
    {
        var dmChannel = await GuildUser.CreateDMChannelAsync();
        await dmChannel.SendMessageAsync(Constants.HelpMessage);
    }
}