using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Data;

namespace ShipperDataVersionView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
            ShipperComboBox.Items.Add("UPS EMT");
            ShipperComboBox.Items.Add("USPS EMT");
        }

        private void FillDataGrid()
        {
            string conString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (var con = new SqlConnection(conString))
            {
                const string cmdString = "SELECT TOP 2 * FROM manifests";
                var cmd = new SqlCommand(cmdString, con);
                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable("dummyTable");
                sda.Fill(dt);
                MyDataGrid.ItemsSource = dt.DefaultView; 
            }
        }
    }

    public class NullToVisibilityConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}