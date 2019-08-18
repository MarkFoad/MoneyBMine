using MBM.BL;
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
using System.Windows.Shapes;

namespace MBM.WPF
{
    /// <summary>
    /// Interaction logic for NewRecord.xaml
    /// </summary>
    public partial class NewRecord : Window
    {
        
        SQL sql = new SQL();
        public NewRecord()
        {
            InitializeComponent();
            sql.AddRecordEventHandler.AddRecordEvent += AddCompletedSuccessEvent;
            sql.AddRecordEventHandler.AddRecordFailedEvent += AddCompletedSuccessEvent;

        }

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

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Stock stock = new Stock();

            stock.StockExchange = tbStockExchange.Text.ToUpper();
            stock.StockSymbol = tbStockSymbol.Text.ToUpper();
            stock.Date = dpDate.SelectedDate.Value;
            stock.StockPriceOpen = double.Parse(tbStockPriceOpen.Text);
            stock.StockPriceHigh = double.Parse(tbStockPriceHigh.Text);
            stock.StockPriceLow = double.Parse(tbStockPriceLow.Text);
            stock.StockPriceClose = double.Parse(tbStockPriceClose.Text);
            stock.StockVolume = int.Parse(tbStockVolume.Text);
            stock.StockPriceAdjClose = double.Parse(tbStockPriceAdjClose.Text);
            sql.AddRecord(stock);
            
        }
    }
}
