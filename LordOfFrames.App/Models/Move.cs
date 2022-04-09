namespace LordOfFrames.App.Models;

public class Move : BaseResource
{
    public string Input { get; set; }
    public int? Startup { get; set; }
    public int? Active { get; set; }
    public int? OnHit { get; set; }
    public int? OnBlock { get; set; }
    public int? Recovery { get; set; }
    public MoveType Type { get; set; }
}

public enum MoveType
{
    Normal,
    CommandNormal,
    Special,
    Super,
    Ultimate,
    InstantKill
}