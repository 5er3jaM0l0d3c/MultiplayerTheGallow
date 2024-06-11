using API;
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
using System.Windows.Threading;
using Client.DestroyerSecretsPages;
using Client.MakerSecretsPages;
using Client.StructuresAndOther;

namespace Client.BothRolePages
{
    /// <summary>
    /// Логика взаимодействия для WaitingPage.xaml
    /// </summary>
    public partial class WaitingPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        Player.Roles player;
        int numOfMistakes;
        public WaitingPage(Player.Roles player, int NumOfMistakes = 0)
        {
            InitializeComponent();

            this.player = player;
            numOfMistakes = NumOfMistakes;
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += TMR_Waiting;
            timer.Start();

        }

        HttpClient client = Manager.client;

        private async void TMR_Waiting(object? sender, EventArgs e)
        {
            var response = await client.GetAsync("http://26.16.166.250:7269/api/Main/IsWaitingPlayer?role=" + player.ToString());
            var result = await response.Content.ReadAsAsync<bool>();

            if (!result)
            {
                timer.Stop();
                
                Manager.MainAreaFrame.Navigate(player == Player.Roles.Destroyer ? new DestroyerMainPage() : new MakerMainPage(numOfMistakes));
            }
        }
    }
}
