﻿namespace LordOfFrames.App;

public static class Constants
{
    public const string BotToken = "CHANGE_ME";
    
    public static readonly string HelpMessage = $"**The following commands can be used:**{Environment.NewLine}" +
                                                $"  • **ping** - Pings the Discord channel{Environment.NewLine}";

    public const string PingMessage = "I am pinging the server.";
    public const string MongoDatabaseUsername = "admin";
    public const string MongoDatabasePassword = "CHANGE_ME";
    public static readonly string MongoDatabaseConnectionString = $"mongodb+srv://{MongoDatabaseUsername}:{MongoDatabasePassword}@cluster0.ws062.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
}