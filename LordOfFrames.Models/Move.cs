using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LordOfFrames.Models;

public class Move : Base<Move>
{
    [Required] public string Input { get; set; }
    public int? Startup { get; set; }
    public int? Active { get; set; }
    public int? OnHit { get; set; }
    public int? OnBlock { get; set; }
    public int? Recovery { get; set; }
    public MoveType Type { get; set; }

    public string MapMoveTypeEnumToDisplayString()
    {
        return Type switch
        {
            MoveType.Normal => "Normal",
            MoveType.CommandNormal => "Command Normal",
            MoveType.Special => "Special",
            MoveType.Super => "Super",
            MoveType.Ultimate => "Ultimate",
            MoveType.InstantKill => "Instant Kill",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
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