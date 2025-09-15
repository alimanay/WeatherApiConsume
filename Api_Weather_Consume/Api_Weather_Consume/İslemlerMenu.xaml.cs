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
        public async void UpdateList()
        {
            string url = "https://localhost:7040/api/Weathers";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                JArray jArray = JArray.Parse(responseBody);
                DataGridList.ItemsSource = jArray;
            }
        }


        private async void ButtonList_ClickAsync(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7040/api/Weathers";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                JArray jArray = JArray.Parse(responseBody);
                DataGridList.ItemsSource = jArray;
            }

        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7040/api/Weathers";

            var neweatherCity = new
            {
                cityName = txt_CityName.Text,
                country = txt_Country.Text,
                temprature = txt_Temprature.Text,
                detail = txt_Detail.Text,
            };
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(neweatherCity);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                    string responsebody = await httpResponseMessage.Content.ReadAsStringAsync();
                    JArray jArray = JArray.Parse(responsebody);
                    

                    if (response.IsSuccessStatusCode)
                    {
                        DataGridList.ItemsSource = jArray;
                        MessageBox.Show("Yeni şehir eklendi!");
                    }
                    else
                    {

                        MessageBox.Show($"Hata:Veriler boş bırakılamaz");
                    }
                }
                catch { }

            }

        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_Id.Text))
                {
                    MessageBox.Show("Lütfen silinecek kaydı seçin!");
                    return;
                }
                string url = $"https://localhost:7040/api/Weathers?id={txt_Id.Text}";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        UpdateList();
                        MessageBox.Show("Seçilen şehir başarıyla silindi");
                        txt_Id.Text = "";
                        txt_CityName.Text = "";
                        txt_Country.Text = "";
                        txt_Temprature.Text = "";
                        txt_Detail.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Geçersiz bir işlem veya silme başarısız");
                    }
                }
            }
            catch
            {
            }
        }

        private void DataGridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridList.SelectedItem is JObject row)
            {
                txt_CityName.Text = row["cityName"]?.ToString();
                txt_Country.Text = row["country"]?.ToString();
                txt_Detail.Text = row["detail"]?.ToString();
                txt_Temprature.Text = row["temprature"]?.ToString();
                txt_Id.Text = row["cityId"]?.ToString();
            }
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://localhost:7040/api/Weathers";
            var newweathercity = new
            {
                cityId = txt_Id.Text,
                cityName = txt_CityName.Text,
                country = txt_Country.Text,
                temprature = txt_Temprature.Text,
                detail = txt_Detail.Text
            };
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(newweathercity);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    UpdateList();
                    MessageBox.Show("Veriler başarıyla güncellendi");
                }
                else
                {
                    MessageBox.Show("Geçersiz İşlem");

                }
            }
        }


    }
}
    


