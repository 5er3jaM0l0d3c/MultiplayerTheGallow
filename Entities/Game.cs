﻿using System;
using System.Collections.Generic;

namespace Entities;

public partial class Game
{
    public int Id { get; set; }

    public string? Word { get; set; }

    public int? MistakesNum { get; set; }

    public int MakerId { get; set; }

    public int? DestroyerId { get; set; }

    public string? LastLetter { get; set; }

    public virtual Player? Destroyer { get; set; }

    public virtual Player Maker { get; set; } = null!;

}
