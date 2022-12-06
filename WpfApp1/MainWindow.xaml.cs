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
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private Dictionary<string, Page> pages = new Dictionary<string, Page>();
        private List<Appointment>[] Appointments;
        private Page DashboardPage = new Dashboard();
        private Page PatientsPage = new Patients();


        public MainWindow()
        {
            List<Appointment> today = new List<Appointment>
            {
                new Appointment("Rupert", "Amodia", "9:00AM", "9:30AM", "Dr. Chirag"),
                new Appointment("Araiz", "Asad", "10:00am", "10:30am", "Dr. Raphael"),
                new Appointment("Elizabeth Chu", "Asad", "11:00am", "10:30am", "Dr. Amr"),
                new Appointment("David", "Smith", "11:00am", "11:30am", "Dr. Raphael")
            };

            this.Appointments = new List<Appointment>[5];
            this.Appointments[0] = today;
            this.Appointments[1] = new List<Appointment>();

            InitializeComponent();

            //MainNavFrame.NavigationService.Navigate(this.DashboardPage);
            //this.DashboardPage.Loaded += DashboardPage_Loaded;

            MainNavFrame.LoadCompleted += MainNavFrame_LoadCompleted;

        }

        private void DashboardPage_Loaded(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("hello dashboard");
        }

        private void MainNavFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Trace.WriteLine(sender.ToString());
        }

        public class APatient
        {
            private string _patientName;
            private string _patientPhoneNumber;
            private string _patientHealthCareNumber;
            private string _patientDOB;
            private string _patientAddress;
            private string _patientPreferredDoctor;
            private string _patientHealthHistory;
            private string _patientConditions;

            public string PatientName
            {
                get { return _patientName; }
                set { _patientName = value; }
            }

            public string PatientPhoneNumber
            {
                get { return _patientPhoneNumber; }
                set { _patientPhoneNumber = value; }
            }

            public string PatientHealthCareNumber
            {
                get { return _patientHealthCareNumber; }
                set { _patientHealthCareNumber = value; }
            }

            public string PatientDOB
            {
                get { return _patientDOB; }
                set { _patientDOB = value; }
            }

            public string PatientAddress
            {
                get { return _patientAddress; }
                set { _patientAddress = value; }
            }

            public string PatientPreferredDoctor
            {
                get { return _patientPreferredDoctor; }
                set { _patientPreferredDoctor = value; }
            }

            public string PatientHealthHistory
            {
                get { return _patientHealthHistory; }
                set { _patientHealthHistory = value; }
            }

            public string PatientConditions
            {
                get { return _patientConditions; }
                set { _patientConditions = value; }
            }

            public override string ToString()
            {
                return this.PatientName;
            }

            public APatient(
                string patientName,
                string patientPhoneNumber,
                string patientHealthCareNumber,
                string patientDOB,
                string patientAddress,
                string patientPreferredDoctor,
                string patientHealthHistory,
                string patientConditions)
            {
                _patientName = patientName;
                _patientPhoneNumber = patientPhoneNumber;
                _patientHealthCareNumber = patientHealthCareNumber;
                _patientDOB = patientDOB;
                _patientAddress = patientAddress;
                _patientPreferredDoctor = patientPreferredDoctor;
                _patientHealthHistory = patientHealthHistory;
                _patientConditions = patientConditions;
            }

        }


        public class Appointment
        {
            private string _firstName;
            private string _lastName;
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
                string start,
                string end,
                string doctor)
            {
                _firstName = firstname;
                _lastName = lastname;
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

            //MainNavFrame.Source = new Uri("Dashboard.xaml", UriKind.Relative);

            MainNavFrame.NavigationService.Navigate(this.DashboardPage);
        }

        private void Patients_Button_Click(object sender, RoutedEventArgs e)
        {
            Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
            Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
            Dashboard_Button.FontWeight = FontWeights.Regular;

            MainNavFrame.NavigationService.Navigate(this.PatientsPage);
        }
    }
}
