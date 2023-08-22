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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;

namespace test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool MenuClosed = true;
        Storyboard openMenu;
        Storyboard closeMenu;


        public MainWindow()
        {
            InitializeComponent();
            startClock();
            Pages.adminManageLeave aml = new Pages.adminManageLeave();
            grid_Menu.Visibility = Visibility.Hidden;
            openMenu = (Storyboard)button.FindResource("OpenMenu");
            closeMenu = (Storyboard)button.FindResource("CloseMenu");

            ProgressBar pb = new ProgressBar();
            mainFrame.Content = new Pages.ProgressBar();

            if(MyConnection.progressValue == 100)
            {
                mainFrame.Content = null;
            }
        }


        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                openMenu.Begin();
                menuIcon.Kind = PackIconKind.ArrowLeft;
                menuIcon.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                closeMenu.Begin();
                menuIcon.Kind = PackIconKind.Menu;
            }

            MenuClosed = !MenuClosed;
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


        private void startClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            datelbl.Text = DateTime.Now.ToString(@"HH':'mm':'ss tt");
            datelbl2.Text = DateTime.Now.ToString(@"dddd, dd/MM/yyyy");
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            homeBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF14182C"));
            adminManageLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            empBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            manageAccBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            applyLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            checkLeaveStatusBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            logoutBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));

            homeIcon.Foreground = new SolidColorBrush(Colors.White);
            manageLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            empIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            manageAccIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            applyLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            checkLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            mainFrame.Content = new Homepage();
        }


        private void checkLeaveStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            checkLeaveStatusBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF14182C"));
            adminManageLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            empBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            manageAccBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            applyLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            homeBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            logoutBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));

            checkLeaveIcon.Foreground = new SolidColorBrush(Colors.White);
            manageLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            empIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            manageAccIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            applyLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            homeIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            mainFrame.Content = new Pages.viewLeaveStatus();
        }

        private void applyLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            applyLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF14182C"));
            adminManageLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            empBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            manageAccBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            checkLeaveStatusBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            homeBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            logoutBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));

            applyLeaveIcon.Foreground = new SolidColorBrush(Colors.White);
            manageLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            empIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            manageAccIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            checkLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            homeIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            mainFrame.Content = new Pages.LeaveApplication();
        }

        private void manageAccBtn_Click(object sender, RoutedEventArgs e)
        {
            manageAccBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF14182C"));
            adminManageLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            empBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            applyLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            checkLeaveStatusBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            homeBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            logoutBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));

            manageAccIcon.Foreground = new SolidColorBrush(Colors.White);
            manageLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            empIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            applyLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            checkLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            homeIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            mainFrame.Content = new Pages.Register();
        }

        private void empBtn_Click(object sender, RoutedEventArgs e)
        {
            empBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF14182C"));
            adminManageLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            manageAccBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            applyLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            checkLeaveStatusBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            homeBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            logoutBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));

            empIcon.Foreground = new SolidColorBrush(Colors.White);
            manageLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            manageAccIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            applyLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            checkLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            homeIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            mainFrame.Content = new CrudPage();
        }

        private void adminManageLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            adminManageLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF14182C"));
            empBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            manageAccBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            applyLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            checkLeaveStatusBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            homeBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            logoutBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));

            manageLeaveIcon.Foreground = new SolidColorBrush(Colors.White);
            empIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            manageAccIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            applyLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            checkLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            homeIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            mainFrame.Content = new Pages.adminManageLeave();
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            adminManageLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            empBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            manageAccBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            applyLeaveBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            checkLeaveStatusBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            homeBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));
            logoutBtn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF212437"));

            manageLeaveIcon.Foreground = new SolidColorBrush(Colors.White);
            empIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            manageAccIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            applyLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            checkLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            homeIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));

            grid_Menu.Visibility = Visibility.Hidden;
            sophicLogo.Visibility = Visibility.Visible;
            mainGridLoginUI.Visibility = Visibility.Visible;
            usernameTxt.Clear();
            passwordTxt.Clear();
            mainFrame.Content = null;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            manageLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            empIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            manageAccIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            applyLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            checkLeaveIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            homeIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));
            menuIcon.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDDDDD"));


            string md5Password = Encrypt(passwordTxt.Password);
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");
            try
            {
                SqlCommand cmd = new SqlCommand("select * from loginTable where Username=@Username AND Password=@Password", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.Parameters.AddWithValue("@Username", usernameTxt.Text);
                cmd.Parameters.AddWithValue("@Password", md5Password);
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    rd.Read();
                    if (rd[4].ToString() == "Admin")
                    {
                        Pages.adminManageLeave aml = new Pages.adminManageLeave();
                        MyConnection.type = "A";
                        applyLeaveBtn.Visibility = Visibility.Collapsed;
                        homeBtn.Visibility = Visibility.Visible;
                        checkLeaveStatusBtn.Visibility = Visibility.Visible;
                        empBtn.Visibility = Visibility.Visible;
                        manageAccBtn.Visibility = Visibility.Visible;
                        adminManageLeaveBtn.Visibility = Visibility.Visible;
                        MyConnection.welcomeMsg = usernameTxt.Text;
                        MyConnection.rollMsg = "Admin";
                    }
                    else if (rd[4].ToString() == "User")
                    {
                        MyConnection.type = "U";
                        homeBtn.Visibility = Visibility.Visible;
                        checkLeaveStatusBtn.Visibility = Visibility.Visible;
                        empBtn.Visibility = Visibility.Hidden;
                        manageAccBtn.Visibility = Visibility.Hidden;
                        applyLeaveBtn.Visibility = Visibility.Visible;
                        adminManageLeaveBtn.Visibility = Visibility.Hidden;  
                        MyConnection.welcomeMsg = usernameTxt.Text;
                        MyConnection.rollMsg = "User";
                    }

                    grid_Menu.Visibility = Visibility.Visible;
                    sophicLogo.Visibility = Visibility.Hidden;
                    mainGridLoginUI.Visibility = Visibility.Hidden;
                    mainFrame.Content = new Homepage();
                }
                else
                {
                    MessageBox.Show("Wrong Login Information", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void cancelLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            usernameTxt.Clear();
            passwordTxt.Clear();
        }


        private async void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            var exitResult = (bool)await DialogHost.Show(new Exit(), "MainDialogHost");
            if (exitResult)
            {
                Environment.Exit(101);
            }
        }
        /* ------------------------------------------------------------------------------------ */

       

        private void grid_Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            //if (!MenuClosed)
            //{ 
            //    closeMenu.Begin();
            //    MenuClosed = true;
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //txtUserName.Focus();

            //if (Config.Global.CheckUpdate.ToLower() == "true")
            {
                // _ await
                // _ //= UpdateIfAvailable();
            }
            //btnManagement.Visibility = Visibility.Visible;


        }

        private void userEditProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Pages.EditProfile();
        }

        private void usernameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2J6G9H0A\SQLEXPRESS;Initial Catalog=InternDB;Integrated Security=True");
            con.Open();

            if (usernameTxt.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT empID from loginTable WHERE Username = '"+usernameTxt.Text+"' ", con);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    MyConnection.employeeID = da.GetValue(0).ToString();
                }
                con.Close();
            }
            else
            {
                MyConnection.employeeID = "";
            }
        }
    }
}
