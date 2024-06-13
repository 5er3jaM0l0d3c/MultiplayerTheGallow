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
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {
        public EnterPage()
        {
            InitializeComponent();
        }


        private void RegistrationTBXClick(object sender, MouseButtonEventArgs e)
        {
            Manager.MainAreaFrame.Navigate(new RegistrationPage());
        }

        private async void EnterBTNCLick(object sender, RoutedEventArgs e)
        {
            HttpClient client = new();
            try
            {
                var response = await client.GetAsync("http://localhost:5279/api/Player/AutorizePlayer?login=" + LoginTBX.Text + "&password=" + PasswordPBX.Password);
                if(!response.IsSuccessStatusCode)
                {
                    ErrorTBX.Visibility = Visibility.Visible; return;
                }
                Manager.Player = await response.Content.ReadAsAsync<Player>();
                Manager.MainAreaFrame.Navigate(new MainMenuPage());
            }
            catch
            {
                    MessageBox.Show("Нет подключения к серверу.");
            }
        }
    }
}
