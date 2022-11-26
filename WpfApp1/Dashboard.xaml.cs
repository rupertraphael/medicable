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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            List<Appointment> appointments = DB.Appointments;

            // say we want to add an appointment to our 'DB'
            appointments.Add(new Appointment("New", "Patient", 1, "3:00PM", "3:30PM", "Dr. Chirag"));

            InitializeComponent();
            quickView.ItemsSource = appointments;
        }
    }
}
