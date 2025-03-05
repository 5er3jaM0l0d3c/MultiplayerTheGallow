using System;
using System.Collections.Generic;

namespace Entities;

public partial class Player
{
    private string text;

    public Player(string login, string password)
    {
        Login = login;
        Password = password;
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
