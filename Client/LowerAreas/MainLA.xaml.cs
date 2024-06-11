using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
using Client.BothRolePages;
using Client.StructuresAndOther;

namespace Client.LowerAreas
{
    /// <summary>
    /// Логика взаимодействия для MainLA.xaml
    /// </summary>
    /// 

    
    public partial class MainLA : Page
    {

        HttpClientHandler httpClientHandler = new HttpClientHandler();

        HttpClient client = new();
        public MainLA()
        {
            InitializeComponent();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient clientt = new HttpClient(httpClientHandler);
            Manager.client = clientt;
            client = clientt;
            CheckConnection();
            
        }

        

        private async Task CheckConnection()
        {
            ConnectLine.Stroke = Brushes.Gray;
            HttpResponseMessage response = new();
            int num;
            try
            {
                response = await client.GetAsync("http://26.16.166.250:7269/api/Main/CheckConnection?num=5");
                num = await response.Content.ReadAsAsync<int>();
                if (num == 5)
                {
                    ConnectLine.Stroke = Brushes.LimeGreen;
                    SPNConnection.Children.Remove(BTNRConnect);
                    Manager.MainAreaFrame.Navigate(new MainMenuPage());
                }
            }
            catch
            {
                BTNRConnect.IsEnabled = true;

                ConnectLine.Stroke = Brushes.Red;
            }          
        }

        private void BTN_RConnect(object sender, RoutedEventArgs e)
        {
            BTNRConnect.IsEnabled = false;
            CheckConnection();
        }
    }
}
