using MBM.BL.Data;
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
    /// Interaction logic for SQLServer.xaml
    /// </summary>
    public partial class SQLServer : Window
    {
        private readonly SQLRepository sqlRepository = SQLRepository.Instance;
        /// <summary>
        /// Gets or Sets the Memory being used.
        /// </summary>
        public double Memory { get; set; }

        public double HDDSpaceFree { get; set; }
        public SQLServer()
        {
            InitializeComponent();
            Load();
        }

        /// <summary>
        /// Populates the SQL  Sever Utilization details on page load.
        /// </summary>
        private async void Load()
        {
            // Returns the HDD Free space form SQL Server 
            HDDSpaceFree = await sqlRepository.GetHDDFree();
            double hddround = Math.Round(HDDSpaceFree * 100) / 100;
            // Retrieves the memory currently being utilized
            Memory = await sqlRepository.GetMemoryUtilization();
            // Rounds to 2 decimal places 
            double memoryround = Math.Round(Memory *100)/100;

            lblHddSpace.Content = $"Hard Disk Drive Space Has {hddround}MB Free ";
            lblMemory.Content = $"Memory Utilization {memoryround}MB of 0 MB";            
        }

        /// <summary>
        /// Closes the currently open page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Refreshes the page information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }
    }
}
