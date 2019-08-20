using MBM.BL;
using MBM.BL.Data;
using MBM.BL.Models;

using Microsoft.Win32;
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

namespace MBM.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CSVRepository csvRepository = CSVRepository.Instance;
        SQLRepository sqlRepository = SQLRepository.Instance;
        AddRecordEventHandler AddRecord = new AddRecordEventHandler();

        public int TotalRecords { get; set; }
        public int RecordCounter { get; set; }
        private AddRecordEventHandler finished = new AddRecordEventHandler();

        public MainWindow()
        {
            InitializeComponent();
            HideProgress();
            AddRecord.AddRecordCounterEvent += IncreaseCounterEvent;
            finished.AddRecordEvent += FinishedEvent;

        }

        /// <summary>
        /// Displays the message box when the SQL Records update has been completed.
        /// Then hides the progress bar and the label indicator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishedEvent(object sender, EventArgs e)
        {
            MessageBox.Show("SQL Update from CSV file completed");
            HideProgress();
        }



        /// <summary>
        /// Hides the progress bar and the progress bar label
        /// </summary>
        private void HideProgress()
        {
            pbProgress.Visibility = Visibility.Hidden;
            lblProgress.Visibility = Visibility.Hidden;
        }

        private void IncreaseCounterEvent(object sender, EventArgs e)
        {
            RecordCounter = AddRecord.RecordCount;
        }

        private async void BtnGetAll_Click(object sender, RoutedEventArgs e)
        {

            List<Stock> stockList = await sqlRepository.GetAll();
            dgDisplay.ItemsSource = stockList;

        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Opens a new window to capture new record details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiAddRecord_Click(object sender, RoutedEventArgs e)
        {
            NewRecord newRecord = new NewRecord();
            newRecord.Show();

        }


        /// <summary>
        /// Sets the Date Format of the Data-grid to dd/MM/YYYY 
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgDisplay_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Date")
            {
                DataGridTextColumn column = e.Column as DataGridTextColumn;
                Binding binding = column.Binding as Binding;
                binding.StringFormat = "dd/MM/yyyy";
            }
        }

        private async void BtnGetAllcsv_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            List<Stock> stockList = await csvRepository.ReadCSV(fileDialog.FileName);
            //    List<Stock> stockList = await csv.ReadCSV($"NYSE_daily_prices_A(sample).csv");
            dgDisplay.ItemsSource = stockList;

        }

        private async void MiUpdateSQL_Click(object sender, RoutedEventArgs e)
        {
            List<Stock> stockList = new List<Stock>();
            OpenFileDialog fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            stockList = await csvRepository.ReadCSV(fileDialog.FileName);
            sqlRepository.AddRecord(stockList);


        }
    }
}
