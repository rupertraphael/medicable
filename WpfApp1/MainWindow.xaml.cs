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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public class Appointment
        {
            private string _firstName;
            private string _lastName;
            private int _day;
            private string _start;
            private string _end;
            private string _doctor;


            public string FirstName
            {
                get { return _firstName; }
                set { _firstName = value; }
            }

            public string LastName
            {
                get { return _lastName; }
                set { _lastName = value; }
            }

            public int Day
            {
                get { return _day; }
                set { _day = value; }
            }

            public string Start
            {
                get { return _start; }
                set { _lastName = value; }
            }

            public string End
            {
                get { return _end; }
                set { _lastName = value; }
            }

            public string StartEnd
            {
                get { return _start + " - " + _end; }
            }

            public string FullName
            {
                get { return _firstName + " " + _lastName;  }
            }

            public string Doctor
            {
                get { return _doctor; }
            }

            public Appointment(
                string firstname,
                string lastname,
                int day,
                string start,
                string end,
                string doctor)
            {
                _firstName = firstname;
                _lastName = lastname;
                _day = day;
                _start = start;
                _end = end;
                _doctor = doctor;
            }
        }

        private void Dashboard_Button_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
            Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
            Patients_Button.FontWeight = FontWeights.Regular;
            MainNavWindow.Source = new Uri("Dashboard.xaml", UriKind.Relative);
        }

        private void Patients_Button_Click(object sender, RoutedEventArgs e)
        {
            Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
            Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
            Dashboard_Button.FontWeight = FontWeights.Regular;
            MainNavWindow.Source = new Uri("Patients.xaml", UriKind.Relative);
        }
    }
}
