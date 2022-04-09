using LordOfFrames.Models.Request;

namespace LordOfFrames.Models;

public class Game : BaseResource
{
    public List<Character> Characters { get; set; }
    public List<SystemMechanic> SystemMechanics { get; set; }
    
    public Game() {}

    public Game(GameCreateRequest request)
    {
        Id = request.Id;
        Slug = request.Id;
        Name = request.Name;
        Description = request.Description;
        Characters = request.Characters;
        SystemMechanics = request.SystemMechanics;
    }
}