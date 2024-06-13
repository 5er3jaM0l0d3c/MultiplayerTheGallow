using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Client.BothRolePages;
using Client.StructuresAndOther;
using Azure;

namespace Client.DestroyerSecretsPages
{
    /// <summary>
    /// Логика взаимодействия для DestroyerMainPage.xaml
    /// </summary>
    public partial class DestroyerMainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public DestroyerMainPage()
        {
            InitializeComponent();
            WriteWord();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TMRAllClear;
            timer.Start();
        }

        private async void toMainMenu()
        {

            timer.Stop();
            SPNMain.Children.Remove(SPNMistakes);
            TBKtoMainMenu.Foreground = Brushes.Black;
            TBNSwitch.Focusable = false;
            BTNCheckLetter.Focusable = false;
            TBKtoMainMenu.Focus();

        }

        private async void TMRAllClear(object? sender, EventArgs e)
        {
            List<Label> labels = new();
            foreach(var item in SPNSecretLetters.Children)
            {
                var lbl = item as Label;
                labels.Add(lbl);
                
            }

            if(!labels.Any(x => x.Content.Equals("*")))
            {
                toMainMenu();
            }

            var response = await client.GetAsync("http://localhost:5279/api/Game/GetMistakes?gameid=" + Manager.GameId);
            var mistakes = await response.Content.ReadAsAsync<int>();

            if (mistakes < 0)
            {
                LBLNumOfMistakes.Content = "-";
            }
            else
            {
                LBLNumOfMistakes.Content = mistakes;
            }

            if (mistakes == -1)
            {
                toMainMenu();
            }
        }

        HttpClient client = new();
        private async void WriteWord()
        {
            var response = await client.GetAsync("http://localhost:5279/api/Game/GetWord?gameid=" + Manager.GameId);
            var word = await response.Content.ReadAsStringAsync();
            for (int i = 0; i < word.Length; i++)
            {
                var lbl = new Label
                {
                    Tag = i + word[i].ToString(),
                    Content = "*",
                    FontSize = 30,
                };
                SPNSecretLetters.Children.Add(lbl);
            }
        }

        private async void CheckLetter()
        {
            var letter = TBXLetter.Text.FirstOrDefault();
            var response = await client.GetAsync("http://localhost:5279/api/Game/CheckLetter?gameid=" + Manager.GameId + "&letter=" + letter.ToString());
            var result = await response.Content.ReadAsAsync<bool>();

            if(result)
            {
                foreach(var item in SPNSecretLetters.Children)
                {
                    var lbl = item as Label;
                    if (lbl.Tag.ToString().Contains(letter.ToString()))
                    {
                        lbl.Content = letter.ToString();
                    }
                }
            }
        }

        /*private async void CheckWord()
        {
            var word = TBXLetter.Text.ToString().ToLower().Trim();

            var jsonContent = JsonConvert.SerializeObject(word);
            var content = new StringContent(jsonContent);
            await client.PostAsync("http://localhost:5279/api/Main/CheckWord?word=" + word, content);

            if(Manager.Game.Word == word)
            {
                foreach (var item in SPNSecretLetters.Children)
                {
                    var lbl = (Label)item;
                    int index = Convert.ToInt32(lbl.Tag);
                    lbl.Content = word[index];
                }
                toMainMenu();
            }
        }*/

        private void BTN_CheckLetter(object sender, RoutedEventArgs e)
        {

            if(TBNSwitch.IsChecked == false)
            {
                CheckLetter();
            }
            else
            {
                //CheckWord();
            }
            TBXLetter.Text = "";
            BTNCheckLetter.IsEnabled = true;

        }

        private void TBN_toLetter(object sender, RoutedEventArgs e)
        {
            var TBN = sender as ToggleButton;
            Content = "Слово";
            TBXLetter.Width = 20;
        }

        private void TBN_toWord(object sender, RoutedEventArgs e)
        {
            var TBN = sender as ToggleButton;
            Content = "Букву";
            TBXLetter.Width = 175;
        }

        private void TBN_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                
                TBNSwitch.IsChecked = !TBNSwitch.IsChecked;
            }
        }

        private void TBK_toMainMenu(object sender, KeyEventArgs e)
        {
            Manager.MainAreaFrame.Navigate(new MainMenuPage());
        }
    }
}
