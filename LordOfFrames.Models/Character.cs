namespace LordOfFrames.Models;

public class Character : Base<Character>
{
    public List<Move> Moves { get; set; }
    
    public override void Map(Character data)
    {
        base.Map(data);
        Moves = data.Moves;
    }
}