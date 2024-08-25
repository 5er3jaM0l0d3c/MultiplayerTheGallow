using Client.StructuresAndOther;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.BothRolePages
{
    /// <summary>
    /// Логика взаимодействия для PlayerStatsPag.xaml
    /// </summary>
    public partial class PlayerStatsPage : Page
    {
        public PlayerStatsPage()
        {
            InitializeComponent();

            GetPlayer();
        }

        HttpClient client = new HttpClient();

        private async void GetPlayer()
        {
            var response = await client.GetAsync("http://localhost:5279/api/Player/GetPlayerId?id=" + Manager.Player.Id);
            Manager.Player = await response.Content.ReadAsAsync<Player>();
            var player = Manager.Player;
            NumOfGamesTBX.Text = (player.MakerPlayTimes + player.DestroyerPlayTimes).ToString();
            MakerGamesNumTBX.Text = player.MakerPlayTimes.ToString() + "\t" + Math.Round(Convert.ToDouble(player.MakerPlayTimes/ (Int32.Parse(NumOfGamesTBX.Text) == 0 ? 1 :1)),2);
            DestroyerGamesNumTBX.Text = player.DestroyerPlayTimes.ToString();

        }
    }
}
