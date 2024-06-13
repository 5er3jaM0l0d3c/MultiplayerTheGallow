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

        HttpClient client = new();
        public MainLA()
        {
            InitializeComponent();
            
        }
    }
}
