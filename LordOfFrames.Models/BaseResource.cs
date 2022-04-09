using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace LordOfFrames.Models;

public abstract class BaseResource : Base
{
    [BsonId] public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [ConcurrencyCheck] public int Version { get; set; }
}