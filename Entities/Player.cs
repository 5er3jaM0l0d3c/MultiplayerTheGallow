using System;
using System.Collections.Generic;

namespace Entities;

public partial class Player
{

    public Player(string Login, string Password)
    {
        this.Login = Login;
        this.Password = Password;
        MakerPlayTimes = 0;
        DestroyerPlayTimes = 0;
        MakerPlayWinningTimes = 0;
        DestroyerPlayWinningTimes = 0;
        IsOnline = true;
        IsPlaying = false;
    }
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int MakerPlayTimes { get; set; }

    public int DestroyerPlayTimes { get; set; }

    public int MakerPlayWinningTimes { get; set; }

    public int DestroyerPlayWinningTimes { get; set; }

    public bool IsOnline { get; set; }

    public bool IsPlaying { get; set; }

}
