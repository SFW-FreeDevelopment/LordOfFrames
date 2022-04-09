using System.ComponentModel.DataAnnotations;

namespace LordOfFrames.Models.Request;

public class GameCreateRequest
{
    [Required] public string Id { get; set; }
    [Required] public string Name { get; set; }
    public string Description { get; set; }
    public List<Character> Characters { get; set; }
    public List<SystemMechanic> SystemMechanics { get; set; }
}