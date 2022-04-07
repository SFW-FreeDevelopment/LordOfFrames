using Discord.Commands;

namespace LordOfFrames.App.Commands;

public class PingCommand : CommandBase
{
    [Command("ping")]
    public async Task HandleCommandAsync()
    {
        await ReplyAsync(Constants.PingMessage);
    }
}