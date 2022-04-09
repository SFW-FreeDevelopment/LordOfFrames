using LordOfFrames.Models;

namespace LordOfFrames.Models;

public class Character : BaseResource
{
    public List<Move> Moves { get; set; }
}