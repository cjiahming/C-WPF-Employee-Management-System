using System;
using System.Collections.Generic;
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
    /// Interaction logic for LeaveApplication.xaml
    /// </summary>
    public partial class LeaveApplication : Page
    {
        public LeaveApplication()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");

        public bool applyLeaveIsValid()
        {
            if (leaveIDSearch_txt.Text == string.Empty)
            {
                MessageBox.Show("Please enter your employee ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (leaveStartDate.SelectedDate == null)
            {
                MessageBox.Show("Please choose your Leave Start Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (leaveEndDate.SelectedDate == null)
            {
                MessageBox.Show("Please choose your Leave End Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (leaveReason_txt.Text == string.Empty)
            {
                MessageBox.Show("Write a reason of applying leave", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (leaveName_txt.Text == string.Empty)
            {
                MessageBox.Show("Employee ID entered does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (leaveDepartment_txt.Text == string.Empty)
            {
                MessageBox.Show("Employee ID entered does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (leaveContact_txt.Text == string.Empty)
            {
                MessageBox.Show("Employee ID entered does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public void clearLeaveInput()
        {
            leaveIDSearch_txt.Clear();
            leaveName_txt.Clear();
            leaveDepartment_txt.Clear();
            leaveContact_txt.Clear();
            leaveStartDate.SelectedDate = null;
            leaveEndDate.SelectedDate = null;
            leaveReason_txt.Clear();
        }

        private void leaveIDSearch_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");
            con.Open();

            if (leaveIDSearch_txt.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT Name, ContactNumber, Department from crudTable WHERE CONVERT(varchar(50), empID) = '" + leaveIDSearch_txt.Text + "' ", con);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    leaveName_txt.Text = da.GetValue(0).ToString();
                    leaveContact_txt.Text = da.GetValue(1).ToString();
                    leaveDepartment_txt.Text = da.GetValue(2).ToString();
                }
                con.Close();
            }
            else
            {
                leaveName_txt.Clear();
                leaveContact_txt.Clear();
                leaveDepartment_txt.Clear();
            }
        }

        private void cancelLeaveInputBtn_Click(object sender, RoutedEventArgs e)
        {
            clearLeaveInput();
        }

        private void applyLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            leaveIDSearch_txt.Text = leaveIDSearch_txt.Text.ToUpper();
            con.Open();
            viewLeaveStatus vls = new viewLeaveStatus();
            MyConnection.rowCount += 1;
            string leave = "L";
            string num = MyConnection.rowCount.ToString("000");
            string leaveID = leave + num;

            SqlCommand cmdCheckExistLeaveApplication = new SqlCommand("SELECT empID from crudTable WHERE empID = '" + leaveIDSearch_txt.Text + "' ", con);
            string checkExistLA = (string)cmdCheckExistLeaveApplication.ExecuteScalar();

            if(checkExistLA == leaveIDSearch_txt.Text)
            {
                try
                {
                    if (applyLeaveIsValid())
                    {
                        if (MessageBox.Show("Are you sure to apply this leave?", "Apply Leave", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO applyLeavesTable VALUES(@leaveID, @empID, @Name, @Department, @ContactNumber, @StartDate, @EndDate, @Reason, @Status)", con);

                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.AddWithValue("@leaveID", leaveID);
                            cmd.Parameters.AddWithValue("@empID", leaveIDSearch_txt.Text);
                            cmd.Parameters.AddWithValue("@Name", leaveName_txt.Text);
                            cmd.Parameters.AddWithValue("@Department", leaveDepartment_txt.Text);
                            cmd.Parameters.AddWithValue("@ContactNumber", leaveContact_txt.Text);
                            cmd.Parameters.AddWithValue("@StartDate", leaveStartDate.SelectedDate);
                            cmd.Parameters.AddWithValue("@EndDate", leaveEndDate.SelectedDate);
                            cmd.Parameters.AddWithValue("@Reason", leaveReason_txt.Text);
                            cmd.Parameters.AddWithValue("@Status", "");

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Your leave has been applied and is waiting for approval", "Applied", MessageBoxButton.OK, MessageBoxImage.Information);
                            clearLeaveInput();
                            Pages.adminManageLeave aml = new Pages.adminManageLeave();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                try
                {
                    MessageBox.Show("The empID entered does not exist", "Not found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

        }
    }
}
