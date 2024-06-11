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
using Client.MakerSecretsPages;
using Client.StructuresAndOther;

namespace Client.BothRolePages
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {

        DispatcherTimer timerr = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(500),

        };
        public MainMenuPage()
        {
            InitializeComponent();
            
            timerr.Tick +=Timerr_Tick;
            timerr.Start();
            
                
        }

        private void Timerr_Tick(object? sender, EventArgs e)
        {
            CheckRole(Player.Roles.Maker);
            CheckRole(Player.Roles.Destroyer);
        }

        HttpClient client = Manager.client;

        private async void CheckRole(Player.Roles role)
        {
            string Role = role.ToString();
            var response = await client.GetAsync("http://localhost:5279/api/Main/CanTakeRole?role=" + Role);
            var result = await response.Content.ReadAsAsync<bool>();   
            if(role == Player.Roles.Maker)
            {
                BTNMake.IsEnabled = result;
            }
            else
            {
                BTNDestroy.IsEnabled = result;
            }
        }
            
        private async void BTN_MakeSecret(object sender, RoutedEventArgs e)
        {
            var response = await client.GetAsync("http://localhost:5279/api/Main/CanTakeRole?role=Maker");
            var result = await response.Content.ReadAsAsync<bool>();
            if(result)
            {
                await client.GetAsync("http://localhost:5279/api/Main/CreatePlayer?role=Maker");
                timerr.Stop();

                Manager.MainAreaFrame.Navigate(new MakeSecretPage());
            }
            else
            {
                MessageBox.Show("Данная роль уже занята :(");
                BTN_DestroySecret(new object(), new RoutedEventArgs());
            }
             
        }

        private async void BTN_DestroySecret(object sender, RoutedEventArgs e)
        {
            var response = await client.GetAsync("http://localhost:5279/api/Main/CanTakeRole?role=Destroyer");
            var result = await response.Content.ReadAsAsync<bool>();
            if (result)
            {
                response = await client.GetAsync("http://localhost:5279/api/Main/CreatePlayer?role=Destroyer");
                timerr.Stop();
                Manager.MainAreaFrame.Navigate(new WaitingPage(Player.Roles.Destroyer));
            }
            else
            {
                MessageBox.Show("Данная роль уже занята :(");
                BTN_MakeSecret(new object(), new RoutedEventArgs());
            }
        }
    }
}
