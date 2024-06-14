using API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.BothRolePages;
using Client.StructuresAndOther;
using System.Windows.Threading;

namespace Client.MakerSecretsPages
{
    /// <summary>
    /// Логика взаимодействия для MakeSecretPage.xaml
    /// </summary>
    public partial class MakeSecretPage : Page
    {
        public MakeSecretPage()
        {
            InitializeComponent();


        }

        HttpClient client = new();
        int numOfMistakes;
        Label lbl = new Label();
        string word = "";
        private async void BTN_SecretWord(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if(btn.Tag.ToString() == "1")
            {

                word = TBXSecretWord.Text;

                TBKTitle.Text = "Введите количество возможных ошибок";
                
                var slider = new Slider
                {
                    Orientation = Orientation.Horizontal,
                    Minimum = 0,
                    Maximum = 33,
                    Value = 0,
                    TickPlacement = TickPlacement.BottomRight,
                    Margin = new Thickness(10),
                    Name = "SLRNumOfMistakes",
                    Width = 400,
                    TickFrequency = 1,


                };

                slider.ValueChanged += SLR_NumOfMistakes;


                lbl.Content = slider.Value;
                lbl.FontSize = 16;
                lbl.Name = "LBLNumOfMistakes";
                lbl.HorizontalAlignment = HorizontalAlignment.Center;

                SPN.Children.Insert(1, lbl);
                SPN.Children.Insert(1, slider);
                SPN.Children.Remove(TBXSecretWord);
                btn.Tag = "";
                return;
            }
            else
            {
                numOfMistakes = Convert.ToInt32(lbl.Content);
                await client.GetAsync("http://localhost:5279/api/Game/SetGame?gameid=" + Manager.GameId + "&word=" + word + "&numOfMistakes=" + numOfMistakes);

                DispatcherTimer timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromMilliseconds(500),
                };

                timer.Tick += CheckDestroyerConnection;

                timer.Start();
            }
        }
        private async void CheckDestroyerConnection(object sender, EventArgs e)
        {
            try
            {
                var response = await client.GetAsync("http://localhost:5279/api/Game/IsDestroyerConnected?GameId=" + Manager.GameId);
                var result = await response.Content.ReadAsAsync<bool>();

                if (result)
                {
                    //показать что этот гений нашелся и подключился
                    (sender as DispatcherTimer).Stop();
                    Manager.MainAreaFrame.Navigate(new MakerMainPage());
                }
            }
            catch
            {
                MessageBox.Show("Нет подключения к серверу.");
            }
        }

        private void SLR_NumOfMistakes(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            lbl.Content = Convert.ToInt32(slider.Value).ToString();
        }
    }
}
