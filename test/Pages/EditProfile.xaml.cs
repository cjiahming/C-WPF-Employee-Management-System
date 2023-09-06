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
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Page
    {
        public EditProfile()
        {
            InitializeComponent();

            MainWindow mw = new MainWindow();
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");
            con.Open();

            //SqlCommand cmd = new SqlCommand("SELECT e.empID, e.Name, e.Age, e.Gender, e.City, e.ContactNumber, e.Department, e.Email, e.Address FROM crudTable e, loginTable l WHERE l.empID = e.empID", con);
            SqlCommand cmd = new SqlCommand("SELECT * FROM crudTable WHERE empID = '"+MyConnection.employeeID+"'", con);

            try
            {
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    empID_txt.Text = da.GetValue(0).ToString();
                    editName_txt.Text = da.GetValue(1).ToString();
                    editAge_txt.Text = da.GetValue(2).ToString();
                    editGender_txt.Text = da.GetValue(3).ToString();
                    editCity_txt.Text = da.GetValue(4).ToString();
                    editContact_txt.Text = da.GetValue(5).ToString();
                    editDepartment_txt.Text = da.GetValue(6).ToString();
                    editEmail_txt.Text = da.GetValue(7).ToString();
                    editAddress_txt.Text = da.GetValue(8).ToString();
                }
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


        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");



        public bool isEditValid()
        {
            if (editName_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Name' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (editAge_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Age' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (editGender_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Gender' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (editCity_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'City' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (editContact_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Contact No.' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (editDepartment_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Department' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (editEmail_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Email' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (editAddress_txt.Text == string.Empty)
            {
                MessageBox.Show("Do not leave 'Address' field empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        private void saveEditProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isEditValid())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE crudTable SET Name = '" + editName_txt.Text + "', Age = '" + editAge_txt.Text + "', Gender = '" + editGender_txt.Text + "', City = '" + editCity_txt.Text + "', ContactNumber = '" + editContact_txt.Text + "', Department = '" + editDepartment_txt.Text + "', Email = '" + editEmail_txt.Text + "', Address = '" + editAddress_txt.Text + "' WHERE empID = '" +empID_txt.Text + "' ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Profile has been updated successfully!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
