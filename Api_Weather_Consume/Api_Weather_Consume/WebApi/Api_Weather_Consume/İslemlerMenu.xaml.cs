using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace Api_Weather_Consume
{
    /// <summary>
    /// İslemlerMenu.xaml etkileşim mantığı
    /// </summary>
    public partial class İslemlerMenu : Window
    {
        public İslemlerMenu()
        {
            InitializeComponent();
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7040/api/Weathers";
            using (HttpClient client  = new HttpClient())
            {
               HttpResponseMessage httpResponse = await client.GetAsync(url);
               string responseBody= await httpResponse.Content.ReadAsStringAsync();
               JArray jArray = JArray.Parse(responseBody);
               DataGridList.ItemsSource = jArray;
            }
            
        }
    }
}
