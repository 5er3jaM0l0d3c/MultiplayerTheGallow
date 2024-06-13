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

namespace Client.DestroyerSecretsPages
{
    /// <summary>
    /// Логика взаимодействия для DestroyerMainPage.xaml
    /// </summary>
    public partial class DestroyerMainPage : Page
    {
       DispatcherTimer timer = new DispatcherTimer();
        int numOfMistakes;
        string LLetter = "";
        public DestroyerMainPage()
        {
            InitializeComponent();
            TakeWord();
            GetNumOfMistakes();

           


            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TMRAllClear;
            timer.Start();
        }

        private async void GetNumOfMistakes()
        {
            var response = await client.GetAsync("http://localhost:5279/api/Main/GetNumOfMistakes");
            numOfMistakes = Convert.ToInt32(await response.Content.ReadAsStringAsync());
            LBLNumOfMistakes.Content = numOfMistakes;
        }

        private async void toMainMenuT()
        {

            timer.Stop();
            SPNMain.Children.Remove(SPNMistakes);
            TBKtoMainMenu.Foreground = Brushes.Black;
            TBNSwitch.Focusable = false;
            BTNCheckLetter.Focusable = false;
            TBKtoMainMenu.Focus();

        }

        private void TMRAllClear(object? sender, EventArgs e)
        {
            var toMainMenu = false;
            foreach(var item in SPNSecretLetters.Children)
            {
                if(((Label)item).Content.ToString() != "*")
                { toMainMenu = true; }
                else { toMainMenu = false; break; }
                
            }
            if(toMainMenu)
            {
                toMainMenuT();
            }          
            
            if(numOfMistakes < 0)
            {
                foreach(var item in SPNSecretLetters.Children)
                {
                    var lbl = (Label)item;
                    if(lbl.Content.ToString() == "*")
                    {
                        lbl.Content = Manager.Game.Word[Convert.ToInt32(lbl.Tag)];
                        lbl.Foreground = Brushes.Red;
                    }
                }
                toMainMenuT();
            }
        }

        HttpClient client = new();
        private async void TakeWord()
        {
            var response = await client.GetAsync("http://localhost:5279/api/Main/GetWord");
            Manager.Game.Word = await response.Content.ReadAsStringAsync();
            for (int i = 0; i < Manager.Game.Word.Length; i++)
            {
                var lbl = new Label
                {
                    Tag = i,
                    Content = "*",
                    FontSize = 30,
                };
                SPNSecretLetters.Children.Add(lbl);
            }
        }

        private async void CheckLetter(string SecretWord)
        {
            var chr = TBXLetter.Text.FirstOrDefault();
            await client.GetAsync("http://localhost:5279/api/Main/WriteLastLetter?letter=" + chr);
                
            if (chr.ToString() == LLetter || SecretWord.Contains(chr))
            {
                ++numOfMistakes;
            }
            LLetter = chr.ToString();
            List<int> pos = new List<int>();

            for (int i = 0; i < SecretWord.Length; i++)
            {
                if (SecretWord[i] == chr)
                {
                    pos.Add(i);
                }
            }

            if (pos.Count != 0)
            {
                foreach (var item in SPNSecretLetters.Children)
                {
                    var lbl = (Label)item;
                   
                    if (lbl.Tag.ToString() == pos.FirstOrDefault().ToString())
                    {
                        lbl.Content = chr;
                        pos.RemoveAt(0);
                    }
                }
                numOfMistakes--;
            }
            else
                numOfMistakes--;
            LBLNumOfMistakes.Content = numOfMistakes;
        }

        private async void CheckWord()
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
                toMainMenuT();
            }
        }

        private void BTN_CheckLetter(object sender, RoutedEventArgs e)
        {
            BTNCheckLetter.IsEnabled = false;

            if(TBNSwitch.IsChecked == false)
            {
                CheckLetter(Manager.Game.Word);
            }
            else
            {
                CheckWord();
            }
            TBXLetter.Text = "";
            Thread.Sleep(1000);
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
