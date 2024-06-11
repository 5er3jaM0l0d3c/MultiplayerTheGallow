using System;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Client.BothRolePages;
using Client.StructuresAndOther;

namespace Client.MakerSecretsPages
{
    /// <summary>
    /// Логика взаимодействия для MakerMainPage.xaml
    /// </summary>
    public partial class MakerMainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        int NumOfMistakes;
      
        public MakerMainPage(int numOfMistakes)
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TMR_TakeLetter;
            timer.Start();
            NumOfMistakes = numOfMistakes;
            LBLNumOfMistakes.Content = ++NumOfMistakes;
            for (int i = 0; i < Manager.SecretWord.Length; i++)
            {
                var lbl = new Label
                {
                    Foreground = Brushes.Black,
                    Name = "lbl" + i,
                    Content = Manager.SecretWord[i],
                    FontSize = 30,
                };
                SPNSecretWord.Children.Add(lbl);
            }
        }

        private async void TMR_TakeLetter(object? sender, EventArgs e)
        {
            
            var response = await client.GetAsync("http://26.16.166.250:7269/api/Main/TakeLastLetter");
            string chr = await response.Content.ReadAsStringAsync();
            
            if(chr.Length > 1 && chr == Manager.SecretWord)
            {
                foreach(var item in SPNSecretWord.Children)
                {
                    var lbl = item as Label;
                    lbl.Foreground = Brushes.Green;
                }
                
            }

            var toMainMenu = true;
            var isLetterTrue = false;

            foreach (var el in SPNSecretWord.Children)
            {
                var lbl = el as Label;
                if (lbl.Content.ToString() == chr.ToString())
                {
                    isLetterTrue = true;
                    lbl.Foreground = Brushes.LimeGreen;
                }
                if (lbl.Foreground == Brushes.Black)
                {
                    toMainMenu = false;
                }
            }
            if (chr != LBLLastLetter.Content.ToString() && !isLetterTrue)
            {
                LBLNumOfMistakes.Content = --NumOfMistakes;
                if (NumOfMistakes < 0)
                {
                    foreach (var item in SPNSecretWord.Children)
                    {
                        var lbl = item as Label;
                        if (lbl.Content.ToString() == "*")
                        {
                            lbl.Foreground = Brushes.Red;
                        }
                    }
                    toMainMenuT();
                }
            }

            LBLLastLetter.Content = chr;

            if (toMainMenu)
            {
                toMainMenuT();
            }
        }
        
        private async void toMainMenuT()
        {
            await client.GetAsync("http://26.16.166.250:7269/api/Main/ReloadAll");
            timer.Stop();
            SPNOther.Children.Remove(SPNMistakes);
            TBXtoMainMenu.Focusable = true;
            TBXtoMainMenu.Foreground = Brushes.Black;
            TBXtoMainMenu.Focus();
        }

        HttpClient client = Manager.client;

        private void TBX_toMainMenu(object sender, KeyEventArgs e)
        {
            Manager.MainAreaFrame.Navigate(new MainMenuPage());

        }
    }
}
