using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Client.BothRolePages;
using Client.StructuresAndOther;
using Entities;

namespace Client.MakerSecretsPages
{
    /// <summary>
    /// Логика взаимодействия для MakerMainPage.xaml
    /// </summary>
    public partial class MakerMainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        HttpClient client = new();


        public MakerMainPage()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TMR_TakeLetter;
            timer.Start();
            GetWord();
            LBLNumOfMistakes.Content = 9;

        }

        private async void GetWord()
        {
            var response = await client.GetAsync("http://localhost:5279/api/Game/GetWord?GameId=" + Manager.GameId);
            var result = await response.Content.ReadAsStringAsync();

            for (int i = 0; i < result.Length; i++)
            {
                var lbl = new Label
                {
                    Foreground = Brushes.Black,
                    Name = "lbl" + i,
                    Content = result[i],
                    FontSize = 30,
                };
                SPNSecretWord.Children.Add(lbl);
            }
        }

        private async void TMR_TakeLetter(object? sender, EventArgs e)
        {

            var response = await client.GetAsync("http://localhost:5279/api/TrueLetter/GetTrueLetter?GameId=" + Manager.GameId);
            var trueLetters = await response.Content.ReadAsAsync<List<TrueLetter>>();
            
          
            foreach(var item in SPNSecretWord.Children)
            {
                var lbl = item as Label;
                if (trueLetters.FirstOrDefault(x => x.Letter == lbl.Content.ToString()) != null)
                    lbl.Foreground = Brushes.LimeGreen;
            }

            response = await client.GetAsync("http://localhost:5279/api/Game/GetLastLetter?gameid=" + Manager.GameId);
            var  lastLetter = await response.Content.ReadAsStringAsync() ?? " ";
            

            LBLLastLetter.Content = lastLetter;


            List<Label> labels = new();

            foreach (var item in SPNSecretWord.Children)
            {
                var lbl = item as Label;
                labels.Add(lbl);
            }

            if(!labels.Any(x => x.Foreground == Brushes.Black))
                toMainMenu();

            response = await client.GetAsync("http://localhost:5279/api/Game/GetMistakes?gameid=" + Manager.GameId);
            var mistakes = await response.Content.ReadAsAsync<int>();

            if(mistakes < 0)
            {
                LBLNumOfMistakes.Content = "-";
            }
            else
            {
                LBLNumOfMistakes.Content = mistakes;
            }

            if(mistakes == -1)
            {
                toMainMenu();
            }
        }
        
        private void toMainMenu()
        {
            timer.Stop();
            SPNOther.Children.Remove(SPNMistakes);
            TBXtoMainMenu.Focusable = true;
            TBXtoMainMenu.Foreground = Brushes.Black;
            TBXtoMainMenu.Focus();
        }

        private void TBX_toMainMenu(object sender, KeyEventArgs e)
        {
            Manager.MainAreaFrame.Navigate(new MainMenuPage());

        }
    }
}
