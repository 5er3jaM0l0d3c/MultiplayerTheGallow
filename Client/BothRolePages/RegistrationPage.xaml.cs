using Client.StructuresAndOther;
using Entities;
using Newtonsoft.Json;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        bool IsPasswordSame = false;

        private async void PasswordChangedPBX(object sender, RoutedEventArgs e)
        {
            ErrorTBX.Visibility = Visibility.Hidden;
            await Task.Delay(500);
            if(PasswordPBX.Password == PasswordConfirmPBX.Password)
            {
                ErrorTBX.Visibility = Visibility.Hidden;
                IsPasswordSame = true;
            }
            else
            {
                ErrorTBX.Visibility = Visibility.Visible;
                IsPasswordSame = false;
            }
        }

        private async void RegistrationBTNCLick(object sender, RoutedEventArgs e)
        {
            if (LoginTBX.Text == "")
            {
                MessageBox.Show("Введите логин.");
                return;
            }
            if (PasswordConfirmPBX.Password == "")
            {
                MessageBox.Show("Введите пароль.");
                return;
            }
            RegistrationBTN.IsEnabled = false;
            HttpClient client = new();
            var player = new Player(LoginTBX.Text, PasswordConfirmPBX.Password);
            var jsonContent = JsonConvert.SerializeObject(player);
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            try
            {
                if (IsPasswordSame)
                {
                    var response = await client.PostAsync("http://localhost:5279/api/Player/AddPlayer", content);
                    var result = response.IsSuccessStatusCode;
                    if (result)
                    {
                        response = await client.GetAsync("http://localhost:5279/api/Player/GetPlayerLP?login=" + player.Login + "&password=" + player.Password);
                        Manager.Player = await response.Content.ReadAsAsync<Player>();
                        Manager.MainAreaFrame.Navigate(new MainMenuPage());
                    }
                }
                RegistrationBTN.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Ошибка регистрации");
                RegistrationBTN.IsEnabled = true;
            }
        }

        private void EnterTBXClick(object sender, MouseButtonEventArgs e)
        {
            Manager.MainAreaFrame.Navigate(new EnterPage());
        }
    }
}
