using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        private async void ButtonList_ClickAsync(object sender, RoutedEventArgs e)
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

        private  async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7040/api/Weathers";

            var neweatherCity = new
            {
                cityName = txt_CityName.Text,
                country = txt_Country.Text,
                temprature = txt_Temprature.Text,
                detail = txt_Detail.Text,
            };
            using (HttpClient client = new HttpClient()) {
                try
                {
                    string json = JsonConvert.SerializeObject(neweatherCity);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                    string responsebody = await httpResponseMessage.Content.ReadAsStringAsync();
                    JArray jArray = JArray.Parse(responsebody);
                    DataGridList.ItemsSource = jArray;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Yeni şehir eklendi!");
                    }
                    else
                    {

                        MessageBox.Show($"Hata:Veriler boş bırakılamaz");
                    }
                }
                catch{ }

            }

        }
    }
}
