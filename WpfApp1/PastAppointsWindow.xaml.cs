using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    public partial class PastAppointsWindow : Window
    {
        public PastAppointsWindow()
        {
            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(new Appointment("Rupert", "Amodia", 1, "9:00AM", "9:30AM", "Dr. Chirag"));
            appointments.Add(new Appointment("Araiz", "Asad", 1, "10:00AM", "10:30AM", "Dr. Raphael"));
            appointments.Add(new Appointment("Elizabeth Chu", "Asad", 1, "11:00AM", "10:30AM", "Dr. Amr"));
            appointments.Add(new Appointment("David", "Smith", 1, "11:00AM", "11:30AM", "Dr. Raphael"));

            InitializeComponent();
            apptView.ItemsSource = appointments;
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var track = ((ListViewItem)sender).Content as Appointment;
            MessageBox.Show(track.FullName);

        }
    }
}
