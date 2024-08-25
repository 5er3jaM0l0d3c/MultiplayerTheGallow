using Client.StructuresAndOther;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Client.LowerAreas
{
    /// <summary>
    /// Логика взаимодействия для BackLowerAreaPage.xaml
    /// </summary>
    public partial class BackLowerAreaPage : Page
    {
        public BackLowerAreaPage()
        {
            InitializeComponent();
            LoginTBK.Text = Manager.Player.Login;
        }

        private void BackBTNClick(object sender, RoutedEventArgs e)
        {
            Manager.MainAreaFrame.GoBack();
            Manager.LowerAreaFrame.GoBack();
        }
    }
}
