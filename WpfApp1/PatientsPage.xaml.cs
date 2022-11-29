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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WpfApp1.MainWindow;
using static WpfApp1.PastAppointsWindow;
using static WpfApp1.HealthHistoryWindow;

namespace WpfApp1
{
    public partial class PatientsPage : Window
    {
        public PatientsPage()
        {
            InitializeComponent();
        }

        void PastAppointmentsDialog(object sender, RoutedEventArgs e)
        {
            PastAppointsWindow PastAppointDialog = new PastAppointsWindow();
            PastAppointDialog.Owner = this;
            PastAppointDialog.ShowDialog();
        }
        void HealthHistoryDialog(object sender, RoutedEventArgs e)
        {
            HealthHistoryWindow PastAppointDialog = new HealthHistoryWindow();
            PastAppointDialog.Owner = this;
            PastAppointDialog.ShowDialog();
        }
    }
}
