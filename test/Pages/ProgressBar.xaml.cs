using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Page, INotifyPropertyChanged
    {
        private BackgroundWorker _bgWorker = new BackgroundWorker();

        private int _workerState;

        public event PropertyChangedEventHandler PropertyChanged;

        public int WorkerState
        {
            get { return _workerState; }
            set
            {
                _workerState = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
                }
            }
        }

        public ProgressBar()
        {
            InitializeComponent();

            DataContext = this;

            _bgWorker.DoWork += (s, e) =>
            {
                for(int i=0; i<=100; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    WorkerState = i;
                }
                WorkerState = MyConnection.progressValue;
                MessageBox.Show("WorkerState is Done");
            };
            _bgWorker.RunWorkerAsync();
        }
    }
}
