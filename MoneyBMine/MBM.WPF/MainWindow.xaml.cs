using MBM.BL.Models;
using MBM.BL.SQL;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnGetAll_Click(object sender, RoutedEventArgs e)
        {
            SQL sql = new SQL();
            List<Stock> stockList = await sql.GetAll();
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
    }
}
