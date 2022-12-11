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
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        // TODO
        // 1. Quickview empty list when search box not empty
        // 2. Implement search
        // 3. Implement click on appointment
        // 4. Group continuous appointments

        private List<Appointment> Appointments = new List<Appointment>();

        private List<int> calendarDays = new List<int>();
        private List<string> Days = new List<string>()
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };
        private DateTime today = DateTime.Parse("2022-12-01 00:00");
        private DateTime quickViewDate;

        public Patients PatientsPageLink = new Patients();


        public Dashboard()
        {
            quickViewDate= DateTime.Parse(today.ToString());
            List<Appointment> appointments = DB.Appointments;

            InitializeComponent();

            renderList();
        }

        private void Book_Existing_Patient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(PatientsPageLink);
        }

        private void Book_New_Patient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new BookForNewPatient());
        }

        private void Quickview_Next(object sender, RoutedEventArgs e)
        {
            quickViewDate = quickViewDate.AddDays(1);
            renderList();
        }

        private void Quickview_Previous(object sender, RoutedEventArgs e)
        {
            quickViewDate = quickViewDate.AddDays(-1);
            renderList();
        }

        private void Quickview_Today(object sender, RoutedEventArgs e)

        {
            quickViewDate = today;
            renderList();
        }

        private void renderList()
        {
            renderList(this.getAppointments());
        }

        private void renderList(List<Appointment> appointments)
        {
            quickView.ItemsSource = appointments;
            quickViewTitle.Text = this.quickViewDate.ToString("D");
            if (today.Date == quickViewDate.Date)
            {
                quickViewSubtitle.Text = "Appointments for Today";
            }
            else
            {
                quickViewSubtitle.Text = "Appointments for ";
            }
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Trace.WriteLine("navigated to dashboard!");

            // do whatever with str, like assign to a view model field, etc.
        }

        private List<Appointment> getAppointments()
        {
            return DB.Appointments.Where(appointment => appointment.StartDate.Date == quickViewDate.Date)
                .OrderBy(appointment => appointment.StartDate)
                .ToList();
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = SearchBar.Text.ToUpper();
            List<Appointment> appointments = getAppointments().Where(
                a => 
                    a.FullName.ToUpper().Contains(input) ||
                    a.Patient.PatientHealthCareNumber.Contains(input) ||
                    a.Patient.PatientPhoneNumber.Contains(input)
            ).ToList();

            renderList(appointments);

        }

        private void quickview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointment appointment = (Appointment)this.quickView.SelectedItem;

            if (appointment != null)
            {
                APatient client = appointment.Patient;

                quickView.UnselectAll();

                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new AppointmentDetailsCancelReschedule(client, appointment));
            }
        }

    }


}
