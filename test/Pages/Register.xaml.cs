using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
            LoadAccGrid();
        }

        

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");

        public void clearAccPageData()
        {
            addEmpID.Clear();
            addAccUsername_txt.Clear();
            addAccPassword_txt.Clear();
            addAccType_txt.SelectedIndex = -1;
            addAccSearch_txt.Clear();
        }


        static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }


        public void LoadAccGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM loginTable", con);
            DataTable dt = new DataTable();

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                accountsDataGrid.ItemsSource = dt.DefaultView;
            }else
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                accountsDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        public bool isValidAddAcc()
        {
            if (addAccUsername_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave Username field blank!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (addAccPassword_txt.Password == string.Empty)
            {
                MessageBox.Show("Do not leave Password field blank!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }

        private void addAccBtn_Click(object sender, RoutedEventArgs e)
        {
            int countRow = (accountsDataGrid.Items.Count) + 1;
            countRow.ToString();
            string acc = "ACC";
            string num = countRow.ToString("000");
            string accID = acc + num;

            string md5Password = Encrypt(addAccPassword_txt.Password);
            try
            {
                if(addAccSearch_txt.Text != string.Empty)
                {
                    MessageBox.Show("Please leave the ID field blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (isValidAddAcc())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO loginTable VALUES(@accID, @empID, @Username, @Password, @Role)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@accID", accID);
                        cmd.Parameters.AddWithValue("@empID", addEmpID.Text);
                        cmd.Parameters.AddWithValue("@Username", addAccUsername_txt.Text);
                        cmd.Parameters.AddWithValue("@Password", md5Password);
                        cmd.Parameters.AddWithValue("@Role", addAccType_txt.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        LoadAccGrid();
                        MessageBox.Show("New account is added!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearAccPageData();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void editAccBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            addAccSearch_txt.Text = addAccSearch_txt.Text.ToUpper();
            string md5Password = Encrypt(addAccPassword_txt.Password);

            SqlCommand cmdCheckExistRegister = new SqlCommand("SELECT accID from loginTable WHERE accID = '" + addAccSearch_txt.Text + "'", con);
            string existAccID = (string)cmdCheckExistRegister.ExecuteScalar();

            if (existAccID == addAccSearch_txt.Text)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE loginTable SET Username = '" + addAccUsername_txt.Text + "', Password = '" + md5Password + "', Role = '" + addAccType_txt.Text + "' WHERE accID = '" + addAccSearch_txt.Text + "' ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record has been edited successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    clearAccPageData();
                    LoadAccGrid();
                }
            }
            else if (addAccSearch_txt.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a Acc ID to edit", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    LoadAccGrid();
                }

            }
            else
            {
                try
                {
                    MessageBox.Show("The accID entered does not exist", "Not found", MessageBoxButton.OK, MessageBoxImage.Error);
                    addAccSearch_txt.Clear();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    LoadAccGrid();
                }
            }
        }

        private void delAccBtn_Click(object sender, RoutedEventArgs e)
        {
            addAccSearch_txt.Text = addAccSearch_txt.Text.ToUpper();
            con.Open();
            SqlCommand cmdCheckExistDelete2 = new SqlCommand("SELECT accID from loginTable WHERE accID = '" + addAccSearch_txt.Text + "' ", con);
            SqlCommand cmd = new SqlCommand("DELETE FROM loginTable WHERE accID = '" + addAccSearch_txt.Text + "' ", con);

            string id = (string)cmdCheckExistDelete2.ExecuteScalar();

            if(id == addAccSearch_txt.Text)
            {
                try
                {
                    if(MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("empID" + " '" + addAccSearch_txt.Text + "'" + " records has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    }              
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    clearAccPageData();
                    LoadAccGrid();
                }
            }
            else if (addAccSearch_txt.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a Acc ID to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    LoadAccGrid();
                }
            }
            else
            {
                try
                {
                    MessageBox.Show("The empID entered is not exist", "Not found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    addAccSearch_txt.Clear();
                    LoadAccGrid();
                }
            }
            
        }

        private void clearInputAccBtn_Click(object sender, RoutedEventArgs e)
        {
            clearAccPageData();
        }

        private void accountsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                addEmpID.Text = row_selected["empID"].ToString();
                addAccUsername_txt.Text = row_selected["Username"].ToString();
                addAccPassword_txt.Password = (row_selected["Password"].ToString());
                addAccType_txt.Text = row_selected["Role"].ToString();
                addAccSearch_txt.Text = row_selected["accID"].ToString();
            }
        }

        /*private void addAccSearch_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");
            con.Open();
            if (addAccSearch_txt.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT empID from loginTable WHERE accID = '" + addAccSearch_txt.Text + "' ", con);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    addEmpID.Text = da.GetValue(0).ToString();
                }
                con.Close();
            }
            else
            {
                addEmpID.Clear();
            }
        }*/
    }
}
