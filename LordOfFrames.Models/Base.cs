using System.ComponentModel.DataAnnotations;

namespace LordOfFrames.Models;

public abstract class Base<TData> where TData : Base<TData>
{
    [Required] public string Slug { get; set; }
    [Required] public string Name { get; set; }
    public string Description { get; set; }

    public virtual void Map(TData data)
    {
        Description = data.Description;
    }
}