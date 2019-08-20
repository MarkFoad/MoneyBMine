using MBM.BL;
using MBM.BL.Data;
using MBM.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MBM.WPF
{
    /// <summary>
    /// Interaction logic for NewRecord.xaml
    /// </summary>
    public partial class NewRecord : Window
    {
        /// <summary>
        /// Instantiates a new instance of the SQL repository
        /// </summary>
        SQLRepository sqlRepository = SQLRepository.Instance; 

        /// <summary>
        /// Initializes the New Record window.
        /// </summary>
        public NewRecord()
        {
            InitializeComponent();
            sqlRepository.AddRecordEventHandler.AddRecordEvent += AddCompletedSuccessEvent;
            sqlRepository.AddRecordEventHandler.AddRecordFailedEvent += AddCompletedSuccessEvent;

        }

        /// <summary>
        /// Message to display when successfully written to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCompletedSuccessEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Record added successfully");
            this.Close();
        }

        /// <summary>
        /// Notification message that updated successfully
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FailedUpdateEvent(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Saves the record to the SQL database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Stock stock = new Stock();
            if (tbStockExchange.Text != null)
            {
                stock.StockExchange = tbStockExchange.Text.ToUpper();
            }
            if (tbStockSymbol.Text != null)
            {
                stock.StockSymbol = tbStockSymbol.Text.ToUpper();
            }
            if (dpDate.SelectedDate.Value != null)
            {

                stock.Date = dpDate.SelectedDate.Value;
            }
            if (tbStockPriceOpen.Text != null)
            {

                stock.StockPriceOpen = double.Parse(tbStockPriceOpen.Text);
            }
            stock.StockPriceHigh = double.Parse(tbStockPriceHigh.Text);
            stock.StockPriceLow = double.Parse(tbStockPriceLow.Text);
            stock.StockPriceClose = double.Parse(tbStockPriceClose.Text);
            stock.StockVolume = int.Parse(tbStockVolume.Text);
            stock.StockPriceAdjClose = double.Parse(tbStockPriceAdjClose.Text);
            sqlRepository.AddRecord(stock);

        }
        /// <summary>
        /// Checks that the entered value is numeric
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Copied from the MSDN Site for WPF Text-box with Decimal numeric value
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        /// <summary>
        /// Closes the Add New Record window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }    
}
