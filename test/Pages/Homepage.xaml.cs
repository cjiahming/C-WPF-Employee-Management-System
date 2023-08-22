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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace test
{
    /// <summary>
    /// Interaction logic for CustomPage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public Homepage()
        {
            InitializeComponent();
            welcomeLabel.Content += MyConnection.welcomeMsg;
            roleLabel.Content += MyConnection.rollMsg;
            startClock();
            DoubleAnimation da = new DoubleAnimation(360, 0, new Duration(TimeSpan.FromSeconds(9)));
            RotateTransform rt = new RotateTransform();
            hrIcon.RenderTransform = rt;
            hrIcon.RenderTransformOrigin = new Point(0.5, 0.5);
            da.RepeatBehavior = RepeatBehavior.Forever;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
            leaveApplicationNotification.Content += (MyConnection.countLeaveApplication).ToString();

            if(MyConnection.type == "U")
            {
                leaveApplicationNotification.Content = "NOT AVAILABLE";
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
            timeLabel.Content = DateTime.Now.ToString(@"h:mm:ss tt");
            dateLabel.Content = DateTime.Now.ToString(@"dddd, dd/MM/yyyy");
        }

        public TextBox usernameTxt
        {
            get { return usernameTxt; }
        }

        
    }
}
