using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace Api_Weather_Consume
{ 
    public partial class MainWindow : Window
    {
       
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_İslemler(object sender, RoutedEventArgs e)
        {
            İslemlerMenu islemlerMenu = new İslemlerMenu();
            islemlerMenu.Show();
            this.Close();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
