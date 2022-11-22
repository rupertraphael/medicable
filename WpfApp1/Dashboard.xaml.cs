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
            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(new Appointment("Rupert", "Amodia", 1, "9:00AM", "9:30AM", "Dr. Chirag"));
            //appointments.Add(new Appointment("Araiz", "Asad", 1, "10:00AM", "10:30AM", "Dr. Raphael"));
            //appointments.Add(new Appointment("Elizabeth Chu", "Asad", 1, "11:00AM", "10:30AM", "Dr. Amr"));
            //appointments.Add(new Appointment("David", "Smith", 1, "11:00AM", "11:30AM", "Dr. Raphael"));
            //appointments.Add(new Appointment("John", "Cena", 1, "12:30PM", "1:00PM", "Dr. Raphael"));
            //appointments.Add(new Appointment("Matthew", "Murdock", 1, "1:00PM", "1:30PM", "Dr. Amr"));
            //appointments.Add(new Appointment("Jennifer", "Walters", 1, "1:00PM", "1:30PM", "Dr. Chirag"));
            //appointments.Add(new Appointment("Muhammad", "Mohammed", 1, "2:00PM", "2:30PM", "Dr. Chirag"));

            InitializeComponent();
            quickView.ItemsSource = appointments;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Patients.xaml", UriKind.Relative));
        }
    }


}
