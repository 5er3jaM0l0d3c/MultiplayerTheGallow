using System;
using System.Collections.Generic;

namespace Entities;

public partial class Game
{
    public int Id { get; set; }

    public string? Word { get; set; }

    public int? Mistakes { get; set; }

    public int? MakerId { get; set; }

    public int Destroyerid { get; set; }

    public virtual Player Destroyer { get; set; } = null!;

    public virtual Player? Maker { get; set; }
}
