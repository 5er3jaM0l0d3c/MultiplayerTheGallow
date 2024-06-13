using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
using API;
using Client.DestroyerSecretsPages;
using Client.MakerSecretsPages;
using Client.StructuresAndOther;
using Entities;

namespace Client.BothRolePages
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        HttpClient client = new();


            
        private void BTN_MakeSecret(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(500),
            };
            timer.Tick += CheckGames;
            timer.Start();
        }

        private async void CheckGames(object sender, EventArgs e)
        {
            try
            {
                var response = await client.GetAsync("http://localhost:5279/api/Game/ConnectMaker?MakerId=" + Manager.Player.Id);
                var result = await response.Content.ReadAsAsync<Game>();
                if (result != null)
                {
                    Manager.Game = result;
                    (sender as DispatcherTimer).Stop();
                    Manager.MainAreaFrame.Navigate(new MakeSecretPage());
                }
            }
            catch
            {
                MessageBox.Show("Нет подключения к серверу.");
            }
        }

        private async void BTN_DestroySecret(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(500),
            };
            timer.Tick += CheckMakerConnection;

            try
            {
                var response = await client.GetAsync("http://localhost:5279/api/Game/AddGame?id=" + Manager.Player.Id);
                Manager.Game = await response.Content.ReadAsAsync<Game>();
                timer.Start();
            }
            catch
            {
                MessageBox.Show("Нет подключения к серверу.");
            }
        }

        private async void CheckMakerConnection(object sender, EventArgs e)
        {
            try
            { 
                var response = await client.GetAsync("http://localhost:5279/api/Game/IsMakerConnected?GameId=" + Manager.Game.Id);
                var result = await response.Content.ReadAsAsync<bool>();

                if(result)
                {
                    Manager.MainAreaFrame.Navigate(new DestroyerMainPage());
                }
            }
            catch
            {
                MessageBox.Show("Нет подключения к серверу.");
            }
        }
    }
}
