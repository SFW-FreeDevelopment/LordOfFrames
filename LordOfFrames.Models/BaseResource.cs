using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace LordOfFrames.Models;

public abstract class BaseResource : Base
{
    
    [Required] [BsonId] public string Id { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }
    [Required] [ConcurrencyCheck] public int Version { get; set; }
}