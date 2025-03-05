using System;
using System.Collections.Generic;

namespace Entities;

public partial class TrueLetter
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public string Letter { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;
}
