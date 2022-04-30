using MongoDB.Bson.Serialization.Attributes;

namespace LordOfFrames.Api.Models;

internal class ApiKey
{
    [BsonId]
    public string Id { get; set; }
    public string Key { get; set; }
    public string Assignee { get; set; }
    public DateTime CreatedAt { get; set; }
}