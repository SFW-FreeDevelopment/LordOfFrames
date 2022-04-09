namespace LordOfFrames.App.Models;

public class Game : BaseResource
{
    public List<Character> Characters { get; set; }
    public List<SystemMechanic> SystemMechanics { get; set; }
}