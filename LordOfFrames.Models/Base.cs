using System.ComponentModel.DataAnnotations;

namespace LordOfFrames.Models;

public abstract class Base
{
    [Required] public string Slug { get; set; }
    [Required] public string Name { get; set; }
    public string Description { get; set; }
}