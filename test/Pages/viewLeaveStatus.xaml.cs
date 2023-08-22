using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace test.Pages
{
    /// <summary>
    /// Interaction logic for viewLeaveStatus.xaml
    /// </summary>
    public partial class viewLeaveStatus : Page
    {
        public viewLeaveStatus()
        {
            InitializeComponent();
            loadGridLeaveStatus();
            MyConnection.rowCount = leaveStatusDataGrid.Items.Count;
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");


        public void loadGridLeaveStatus()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM applyLeavesTable", con);
            DataTable dt = new DataTable();

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                leaveStatusDataGrid.ItemsSource = dt.DefaultView;
            }
            else
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                leaveStatusDataGrid.ItemsSource = dt.DefaultView;
            }
        }



        private void searchLeaveStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            searchLeaveStatus.Text = searchLeaveStatus.Text.ToUpper();
            con.Open();
            SqlCommand cmdCheckExistLeaveStatus = new SqlCommand("SELECT CONVERT(varchar(50), leaveID) FROM applyLeavesTable WHERE CONVERT (varchar(50), leaveID) = '" + searchLeaveStatus.Text + "' ", con);
            string checkExistLeaveStatus = (string)cmdCheckExistLeaveStatus.ExecuteScalar();

            if (checkExistLeaveStatus == searchLeaveStatus.Text)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM applyLeavesTable WHERE leaveID = '" + searchLeaveStatus.Text + "' ", con);
                try
                {
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    leaveStatusDataGrid.ItemsSource = dt.DefaultView;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    searchLeaveStatus.Clear();
                }
            }
            else if (searchLeaveStatus.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a Leave ID to search", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    loadGridLeaveStatus();
                }
            }
            else
            {
                try
                {
                    MessageBox.Show("The leave ID entered does not exist", "Not found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    searchLeaveStatus.Clear();
                    loadGridLeaveStatus();
                }
            }
        }

        private void searchByDateBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateFrom = DateTime.Parse(searchFromDate.Text);
            DateTime dateTo = DateTime.Parse(searchToDate.Text);

            SqlDataAdapter sdf = new SqlDataAdapter("SELECT * FROM applyLeavesTable WHERE ((CONVERT(varchar(50),StartDate) BETWEEN '" + dateFrom.ToString("yyyy-MM-dd") + "00:00:00" + "' AND '" + dateTo.ToString("yyyy-MM-dd") + "23:59:59" + "') OR (CONVERT(varchar(50),EndDate) BETWEEN '" + dateFrom.ToString("yyyy-MM-dd") + "00:00:00" + "'AND '" + dateTo.ToString("yyyy-MM-dd") + "23:59:59" + "')) ", con);
            DataTable sd = new DataTable();
            sdf.Fill(sd);
            leaveStatusDataGrid.ItemsSource = sd.DefaultView;
        }
    }
}
