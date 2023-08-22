using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace test.Pages
{
    /// <summary>
    /// Interaction logic for adminManageLeave.xaml
    /// </summary>
    public partial class adminManageLeave : Page
    {
        public adminManageLeave()
        {
            InitializeComponent();
            adminLoadLeaveGrid();
            MyConnection.countLeaveApplication = adminManageLeaveDataGrid.Items.Count;
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");

        public bool adminManageLeaveIsValid()
        {
            if(adminSearchLID.Text == string.Empty)
            {
                MessageBox.Show("Please enter a Leave ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        public void adminManageLeaveClearInput()
        {
            adminSearchLID.Clear();
        }


        public void adminLoadLeaveGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM applyLeavesTable WHERE Status = ''", con);
            DataTable dt = new DataTable();

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                adminManageLeaveDataGrid.ItemsSource = dt.DefaultView;
            }
            else
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                adminManageLeaveDataGrid.ItemsSource = dt.DefaultView;
            }
        }


        private void adminApproveLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            adminSearchLID.Text = adminSearchLID.Text.ToUpper();
            adminSearchLID.Text = adminSearchLID.Text.ToUpper();
            con.Open();
            string approveBtn = "Approved";
            SqlCommand cmd = new SqlCommand("UPDATE applyLeavesTable SET Status = '" + approveBtn + "' WHERE leaveID = '" + adminSearchLID.Text + "' ", con);
            SqlCommand cmdCheckExistApprove = new SqlCommand("SELECT leaveID FROM applyLeavesTable WHERE leaveID = '" + adminSearchLID.Text + "' ", con);
            string searchExistLeaveIDApprove = (string)cmdCheckExistApprove.ExecuteScalar();

            if(searchExistLeaveIDApprove == adminSearchLID.Text)
            {
                try
                {
                    if (adminManageLeaveIsValid())
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Leave Approved!", "Approved", MessageBoxButton.OK, MessageBoxImage.Information);
                        MyConnection.countLeaveApplication -= 1;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                    adminLoadLeaveGrid();
                }
            }
            else if(adminSearchLID.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a Leave ID to approve", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                    adminLoadLeaveGrid();
                }
            }
            else
            {
                try
                {
                    MessageBox.Show("Leave ID entered does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                    adminLoadLeaveGrid();
                }
            }      
        }

        private void adminRejectLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            adminSearchLID.Text = adminSearchLID.Text.ToUpper();
            con.Open();
            string rejectBtn = "Rejected";
            SqlCommand cmd = new SqlCommand("UPDATE applyLeavesTable SET Status = '" + rejectBtn + "' WHERE leaveID = '" + adminSearchLID.Text + "' ", con);
            SqlCommand cmdCheckExistReject = new SqlCommand("SELECT leaveID FROM applyLeavesTable WHERE leaveID = '" + adminSearchLID.Text + "' ", con);
            string searchExistLeaveIDReject = (string)cmdCheckExistReject.ExecuteScalar();

            if (searchExistLeaveIDReject == adminSearchLID.Text)
            {
                try
                {
                    if (adminManageLeaveIsValid())
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Leave Rejected!", "Rejected", MessageBoxButton.OK, MessageBoxImage.Information);
                        MyConnection.countLeaveApplication -= 1;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                    adminLoadLeaveGrid();
                }
            }
            else if (adminSearchLID.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a Leave ID to reject", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                    adminLoadLeaveGrid();
                }
            }
            else
            {
                try
                {
                    MessageBox.Show("Leave ID entered does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                    adminLoadLeaveGrid();
                }
            }
        }

        private void adminSearchLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            adminSearchLID.Text = adminSearchLID.Text.ToUpper();
            con.Open();
            SqlCommand cmdCheckExistSearch = new SqlCommand("SELECT leaveID FROM applyLeavesTable WHERE leaveID = '" + adminSearchLID.Text + "' ", con);
            string searchExistLeaveID = (string)cmdCheckExistSearch.ExecuteScalar();

            if(searchExistLeaveID == adminSearchLID.Text)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM applyLeavesTable WHERE leaveID = '" + adminSearchLID.Text + "' ", con);
                try
                {
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    adminManageLeaveDataGrid.ItemsSource = dt.DefaultView;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                }
            }
            else if(adminSearchLID.Text == string.Empty)
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
                    adminManageLeaveClearInput();
                }
            }

            else
            {
                try
                {
                    MessageBox.Show("Leave ID entered does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    adminManageLeaveClearInput();
                }
            }
        }

        private void adminManageLeaveDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                adminSearchLID.Text = row_selected["leaveID"].ToString();
            }
        }
    }
}
