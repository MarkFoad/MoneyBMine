using MBM.WPFExternal.Data;
using MBM.WPFExternal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace MBM.WPFExternal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateForm();
        }


        private void PopulateForm()
        {
            
            HttpClient client;
            HttpResponseMessage response;
           // GetAllRecords(out client, out response);
            PopulateDateComboBox(out client, out response);
            //cbDate.ItemsSource = await stockRepository.GetDate();
        }

        /// <summary>
        /// Populates the Start Date and Finish Date Combo Boxes
        /// </summary>
        /// <param name="client"></param>
        /// <param name="response"></param>
        private void PopulateDateComboBox(out HttpClient client, out HttpResponseMessage response)
        {
            client = APIconnect();
            response = client.GetAsync("api/stocks/GetDates").Result;
            if (response.IsSuccessStatusCode)
            {
                var dates = response.Content.ReadAsAsync<List<string>>().Result;

                cbStartDate.ItemsSource = dates;
                cbFinishDate.ItemsSource = dates;
            }
            else
            {
                MessageBox.Show($"Error Code {response.StatusCode} : Message - {response.ReasonPhrase}" );
            }
        }

        /// <summary>
        /// List of all Stock Records from API
        /// </summary>
        /// <param name="client"></param>
        /// <param name="response"></param>
        private void GetAllRecords(out HttpClient client, out HttpResponseMessage response)
        {
            client = APIconnect();
            response = client.GetAsync("api/stocks").Result;
            if (response.IsSuccessStatusCode)
            {
                List<Stock> stocks = response.Content.ReadAsAsync<List<Stock>>().Result;

                dgDisplay.ItemsSource = stocks;
            }
            else
            {
                MessageBox.Show($"Error Code {response.StatusCode} : Message - {response.ReasonPhrase}");
            }
        }
        /// <summary>
        /// API connection
        /// </summary>
        /// <returns></returns>
        private static HttpClient APIconnect()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
