using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LordOfFrames.Database;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace LordOfFrames.App;

public static class Bot
{
    private static DiscordSocketClient _client;
    private static CommandService _commands;
    private static IServiceProvider _services;
    

    public static async Task RunBotAsync()
    {
        _client = new DiscordSocketClient();
        _commands = new CommandService();
        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddScoped<IMongoClient, MongoClient>(_ => new MongoClient(MongoClientSettings.FromConnectionString(Constants.MongoDatabaseConnectionString)))
            .AddScoped<GameRepository>()
            .BuildServiceProvider();

        const string token = $"{Constants.BotToken}";

        _client.Log += Log;

        await RegisterCommandsAsync();

        await _client.LoginAsync(TokenType.Bot, token);

        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private static Task Log(LogMessage arg)
    {
        Console.WriteLine(arg);
        return Task.CompletedTask;
    }
        
    private static async Task RegisterCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
    }

    private static async Task HandleCommandAsync(SocketMessage socketMessage)
    {
        var message = socketMessage as SocketUserMessage;
        var context = new SocketCommandContext(_client, message);
        if (message is null || message.Author.IsBot) return;

        int argPos = 0;
        if (message.HasStringPrefix("lordofframes ", ref argPos))
        {
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
                Console.WriteLine(result.ErrorReason);
        }
        else
        {
            argPos = 0;
            if (message.HasStringPrefix("<@961429965570719746> ", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}