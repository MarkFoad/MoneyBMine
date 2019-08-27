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
        //AddRecordEventHandler AddRecord = new AddRecordEventHandler();



        private List<Stock> stockList { get; set; }

        public string FilterSelection { get; set; }

        public int TotalRecords { get; set; }
        public int RecordCounter { get; set; }
        private AddRecordEventHandler finished = new AddRecordEventHandler();
        private WaitEventHandler waitstart = new WaitEventHandler();

        public MainWindow()
        {
            InitializeComponent();
            HideProgress();
            sqlRepository.AddRecordEventHandler.AddRecordCounterEvent += ShowProgressCounterEvent;
            finished.AddRecordEvent += FinishedEvent;

            waitstart.Waiter += WaiterStart;

            HideFilterOptions();

            LoadData();

        }

        private void WaiterStart(object sender, EventArgs e)
        {
            MessageBox.Show("Waiter Start");
        }

        private async void LoadData()
        {
            cbStartDate.ItemsSource = await sqlRepository.GetDates();
            cbFinishDate.ItemsSource = cbStartDate.ItemsSource;

        }

        /// <summary>
        /// Hides all the filter options 
        /// </summary>
        private void HideFilterOptions()
        {
            lblFinishDate.Visibility = Visibility.Hidden;
            cbFinishDate.Visibility = Visibility.Hidden;
            lblStartDate.Visibility = Visibility.Hidden;
            cbStartDate.Visibility = Visibility.Hidden;
            btnSearch.Visibility = Visibility.Hidden;
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

        /// <summary>
        /// record Counter for when Progress bar when bulk upload to database
        /// </summary>
        public int recordCount = 0;

        /// <summary>
        /// Displays the Progress bar on the page while bulk upload to database is in progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProgressCounterEvent(object sender, EventArgs e)
        {
            pbProgress.Visibility = Visibility.Visible;
            lblProgress.Visibility = Visibility.Visible;
            pbProgress.Maximum = stockList.Count;
            recordCount++;
            if (pbProgress.Value <= pbProgress.Maximum)
            {
                int percentComplete = (int)Math.Round((double)(100 * recordCount) / pbProgress.Maximum);
                lblProgress.Content = $"{percentComplete}% Complete - Please wait until update is complete!";
                pbProgress.Value = recordCount;
            }
            if (pbProgress.Value == pbProgress.Maximum)
            {
                finished.AddCompleteEvent();
            }

        }

        /// <summary>
        /// Displays all records from the SQL database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnGetAll_Click(object sender, RoutedEventArgs e)
        {


            dgDisplay.ItemsSource = await sqlRepository.GetAll();

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

        /// <summary>
        /// Locate a file to update records in the SQL Database from CSV file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MiUpdateSQL_Click(object sender, RoutedEventArgs e)
        {
            //List<Stock> stockList = new List<Stock>();
            OpenFileDialog fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            stockList = await csvRepository.ReadCSV(fileDialog.FileName);
            sqlRepository.AddRecord(stockList);


        }

        /// <summary>
        ///  Date selection and search button become visible 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiBydate_Click(object sender, RoutedEventArgs e)
        {
            FilterSelection = "GetByDate";
            lblFinishDate.Visibility = Visibility.Hidden;
            cbFinishDate.Visibility = Visibility.Hidden;
            lblStartDate.Content = "Select a date";
            lblStartDate.Visibility = Visibility.Visible;
            cbStartDate.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;


        }
        private void MiBetweenDates_Click(object sender, RoutedEventArgs e)
        {
            FilterSelection = "GetBetweenDates";
            lblFinishDate.Visibility = Visibility.Visible;
            cbFinishDate.Visibility = Visibility.Visible;
            lblStartDate.Content = "Select a start date";
            lblStartDate.Visibility = Visibility.Visible;
            cbStartDate.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Searches for records matching the parameters selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (FilterSelection != string.Empty)
            {

                switch (FilterSelection)
                {
                    case "GetByDate":
                        dgDisplay.ItemsSource = await sqlRepository.GetByDate(DateTime.Parse(cbStartDate.Text));
                        break;
                    case "GetBetweenDates":
                    dgDisplay.ItemsSource = await sqlRepository.GetByDate(DateTime.Parse(cbStartDate.Text), DateTime.Parse(cbFinishDate.Text));
                        break;

                    case "":
                        break;

                }
            }

           
        }

    }
}
