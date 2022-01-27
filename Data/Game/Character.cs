using System.ComponentModel.DataAnnotations;

namespace HelloMUDWorld.Data.Game;

public class Character
{
    [Key]
    public int CharacterId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public int Level { get; set; }
}