using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LordOfFrames.Models;

public class Move : Base<Move>
{
    public string Input { get; set; }
    public int? Startup { get; set; }
    public int? Active { get; set; }
    public int? OnHit { get; set; }
    public int? OnBlock { get; set; }
    public int? Recovery { get; set; }
    public MoveType? Type { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum MoveType
{
    Normal,
    CommandNormal,
    Special,
    Super,
    Ultimate,
    InstantKill
}