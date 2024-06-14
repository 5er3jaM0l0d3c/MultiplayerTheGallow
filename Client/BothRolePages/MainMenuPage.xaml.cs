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
        bool IsCancelSearchGame = false;
        public void CancelSearchGame()
        {
            BTNDestroy.IsEnabled = true;
            BTNMake.IsEnabled = true;
            Manager.LowerArea.LoginSPN.Visibility = Visibility.Visible;
            Manager.LowerArea.CancelGameSPN.Visibility = Visibility.Hidden;
            IsCancelSearchGame = true;
        }


        private  void BTN_DestroySecret(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(500),
            };
            timer.Tick += CheckGames;
            timer.Start();
            Manager.LowerArea.LoadingTBK.Text = "Поиск свободной комнаты...";
            Manager.LowerArea.LoadingSPN.Visibility = Visibility.Visible;

            BTNDestroy.IsEnabled = false;
            BTNMake.IsEnabled = false;

            Manager.LowerArea.CancelBTN.IsEnabled = true;
            Manager.LowerArea.LoginSPN.Visibility = Visibility.Hidden;
            Manager.LowerArea.CancelGameSPN.Visibility = Visibility.Visible;
        }

        private async void CheckGames(object sender, EventArgs e)
        {

            if (IsCancelSearchGame)
            {
                (sender as DispatcherTimer).Stop();
                IsCancelSearchGame = false;
            }
            try
            {
                var response = await client.GetAsync("http://localhost:5279/api/Game/ConnectDestroyer?DestroyerId=" + Manager.Player.Id);
                var result = await response.Content.ReadAsAsync<int>();
                if (result != 0)
                {
                    
                    Manager.GameId = result;
                    (sender as DispatcherTimer).Tick -=CheckGames;
                    (sender as DispatcherTimer).Tick +=IsGameSetted;
                    Manager.LowerArea.CancelBTN.IsEnabled = false;
                    Manager.LowerArea.LoadingTBK.Text = "Ожидание оппонента...";
                }
            }
            catch
            {
                MessageBox.Show("Нет подключения к серверу.");
            }
        }
        private async void IsGameSetted(object? sender, EventArgs e)
        {
            var response = await client.GetAsync("http://localhost:5279/api/game/IsGameSetted?gameid=" + Manager.GameId);
            var result = await response.Content.ReadAsAsync<bool>();

            if(result)
            {
                Manager.LowerArea.LoadingSPN.Visibility = Visibility.Hidden;
                (sender as DispatcherTimer).Stop();

                Manager.LowerArea.LoginSPN.Visibility = Visibility.Visible;
                Manager.LowerArea.CancelGameSPN.Visibility = Visibility.Hidden;
                BTNDestroy.IsEnabled = true;
                BTNMake.IsEnabled = true;

                Manager.MainAreaFrame.Navigate(new DestroyerMainPage());
            }
        }
        //----------------------------------------- <<< ГРАНИЦА ФУНКЦИЙ ДЛЯ ИГРОКОВ >>> --------------------------------------
        private async void BTN_MakeSecret(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await client.GetAsync("http://localhost:5279/api/Game/AddGame?id=" + Manager.Player.Id);
                Manager.GameId = await response.Content.ReadAsAsync<int>();
            }
            catch
            {
                MessageBox.Show("Нет подключения к серверу.");
            }

            Manager.MainAreaFrame.Navigate(new MakeSecretPage());

            
        }
    }
}
