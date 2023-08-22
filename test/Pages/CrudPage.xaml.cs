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

namespace test
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class CrudPage : Page
    {
        public CrudPage()
        {
            InitializeComponent();
            LoadGrid();
        }

        

        public void clearData()
        {
            name_txt.Clear();
            age_txt.Clear();
            gender_txt.SelectedIndex = -1;
            city_txt.Clear();
            contact_txt.Clear();
            search_txt.Clear();
            department_txt.SelectedIndex = -1;
            email_txt.Clear();
            address_txt.Clear();
        }

        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM crudTable", con);
            DataTable dt = new DataTable();

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                datagrid.ItemsSource = dt.DefaultView;
            }
            else
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                con.Close();
                datagrid.ItemsSource = dt.DefaultView;
            }
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");

        public bool isValid()
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Name' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (age_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Age' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (gender_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Gender' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (city_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'City' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (contact_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Contact No.' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (department_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Department' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (email_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Email' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (address_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Address' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        


        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            int countRow = (datagrid.Items.Count) + 1;
            countRow.ToString();
            string emp = "EMP";
            string num2 = countRow.ToString("000");
            string emp2 = emp + num2;

            try
            {
                if(search_txt.Text != string.Empty)
                {
                    MessageBox.Show("Please leave the ID field blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO crudTable VALUES(@empID, @Name, @Age, @Gender, @City, @ContactNumber, @Department, @Email, @Address)", con);

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@empID", emp2);
                        cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                        cmd.Parameters.AddWithValue("@Age", age_txt.Text);
                        cmd.Parameters.AddWithValue("@Gender", gender_txt.Text);
                        cmd.Parameters.AddWithValue("@City", city_txt.Text);
                        cmd.Parameters.AddWithValue("@ContactNumber", contact_txt.Text);
                        cmd.Parameters.AddWithValue("@Department", department_txt.Text);
                        cmd.Parameters.AddWithValue("@Email", email_txt.Text);
                        cmd.Parameters.AddWithValue("@Address", address_txt.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        LoadGrid();
                        MessageBox.Show("Record has been inserted successfully!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearData();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                con.Close();
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();

            /*if (File.Exists("printPreview.xps"))
            {
                File.Delete("printPreview.xps");
            }
            var xpsDocument = new XpsDocument("printPreview.xps", FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)FD).DocumentPaginator);
            Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();
            var windows = new PrintWindow(Document);
            windows.ShowDialog();*/
        }

        //public FixedDocumentSequence Document { get; set; }


        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            search_txt.Text = search_txt.Text.ToUpper();
            SqlCommand cmdCheckExistEmpID = new SqlCommand("SELECT empID from crudTable WHERE empID = '" + search_txt.Text + "' ", con);

            string existEmpID = (string)cmdCheckExistEmpID.ExecuteScalar();

            if(existEmpID == search_txt.Text)
            {
                if (isValid())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE crudTable SET Name = '" + name_txt.Text + "', Age = '" + age_txt.Text + "', Gender = '" + gender_txt.Text + "', City = '" + city_txt.Text + "', ContactNumber = '" + contact_txt.Text + "', Department = '" + department_txt.Text + "', Email = '" + email_txt.Text + "', Address = '" + address_txt.Text + "' WHERE empID = '" + search_txt.Text + "' ", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record has been updated successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        clearData();
                        LoadGrid();
                    }
                }
                else
                {
                    con.Close();
                    LoadGrid();
                }
            }
            else if (search_txt.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a emp ID to update", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    LoadGrid();
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
                    LoadGrid();
                }
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            search_txt.Text = search_txt.Text.ToUpper();
            con.Open();
            SqlCommand cmdCheckExistEmpID = new SqlCommand("SELECT empID from crudTable WHERE empID = '" + search_txt.Text + "' ", con);
            string existEmpID = (string)cmdCheckExistEmpID.ExecuteScalar();

            if(existEmpID == search_txt.Text)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM crudTable WHERE empID = '" + search_txt.Text + "' ", con);
                try
                {
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    datagrid.ItemsSource = dt.DefaultView;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    clearData();
                }
            }
            else if (search_txt.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a emp ID to search", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    LoadGrid();
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
                    clearData();
                    LoadGrid();
                }
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            search_txt.Text = search_txt.Text.ToUpper();
            con.Open();
            SqlCommand cmdCheckExistDelete = new SqlCommand("SELECT empID from crudTable WHERE empID = '" + search_txt.Text + "' ", con);
            string pid = (string)cmdCheckExistDelete.ExecuteScalar();

            if (pid == search_txt.Text)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM crudTable WHERE empID = '" + search_txt.Text+ "' ", con);
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    clearData();
                    LoadGrid();
                }
            }
            else if (search_txt.Text == string.Empty)
            {
                try
                {
                    MessageBox.Show("Enter a Leave ID to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    LoadGrid();
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
                    clearData();
                    LoadGrid();
                }
            }
        }


        private void name_txt_KeyDown(object sender, KeyEventArgs e)
        {      
            // Allow alphanumeric and space.
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Space)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // If tab is presses, then the focus must go to the
            // next control.
            if (e.Key == Key.Tab)
            {
                e.Handled = false;
            }
        }

        private void age_txt_KeyDown(object sender, KeyEventArgs e)
        {         
            // Allow alphanumeric and space.
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)              
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // If tab is presses, then the focus must go to the
            // next control.
            if (e.Key == Key.Tab)
            {
                e.Handled = false;
            }
        }

        private void city_txt_KeyDown(object sender, KeyEventArgs e)
        {
            // Allow alphanumeric and space.
            if (e.Key >= Key.D0 && e.Key <= Key.D9 ||
                e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
                e.Key >= Key.A && e.Key <= Key.Z ||
                e.Key == Key.Space)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // If tab is presses, then the focus must go to the
            // next control.
            if (e.Key == Key.Tab)
            {
                e.Handled = false;
            }
        }

        private void contact_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 ||
                e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
                e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // If tab is presses, then the focus must go to the
            // next control.
            if (e.Key == Key.Tab)
            {
                e.Handled = false;
            }
        }


        /*private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");
            con.Open();

            if (search_txt.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT Name, Age, Gender, City, ContactNumber, Department, Email, Address from crudTable WHERE empID = '" + search_txt.Text + "' ", con);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    name_txt.Text = da.GetValue(0).ToString();
                    age_txt.Text = da.GetValue(1).ToString();
                    gender_txt.Text = da.GetValue(2).ToString();
                    city_txt.Text = da.GetValue(3).ToString();
                    contact_txt.Text = da.GetValue(4).ToString();
                    department_txt.Text = da.GetValue(5).ToString();
                    email_txt.Text = da.GetValue(6).ToString();
                    address_txt.Text = da.GetValue(7).ToString();
                }
                con.Close();
            }
            else
            {
                name_txt.Clear();
                age_txt.Clear();
                gender_txt.SelectedIndex = -1;
                city_txt.Clear();
                contact_txt.Clear();
                department_txt.SelectedIndex = -1;
                email_txt.Clear();
                address_txt.Clear();
            }
        }*/

        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDG print = new PrintDG();

            print.printDG(datagrid, "Employee Information Report");
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                search_txt.Text = row_selected["empID"].ToString();
                name_txt.Text = row_selected["Name"].ToString();
                age_txt.Text = row_selected["Age"].ToString();
                gender_txt.Text = row_selected["Gender"].ToString();
                city_txt.Text = row_selected["City"].ToString();
                contact_txt.Text = row_selected["ContactNumber"].ToString();
                department_txt.Text = row_selected["Department"].ToString();
                email_txt.Text = row_selected["Email"].ToString();
                address_txt.Text = row_selected["Address"].ToString();
            }

        }
    }
}
