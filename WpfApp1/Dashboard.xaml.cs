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


        public Dashboard()
        {
            quickViewDate= DateTime.Parse(today.ToString());

            InitializeComponent();

            renderList();
        }

        private void Book_Existing_Patient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Patients(), "patients");
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

        private void renderList()
        {
            quickView.ItemsSource = this.getAppointments();
            quickViewTitle.Text = this.quickViewDate.ToString("D");
            if(today.Date == quickViewDate.Date)
            {
                quickViewSubtitle.Text = "Appointments for Today";
            } else
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
            // TODO: add end date
            List<Appointment> Appointments = new List<Appointment>
            {
                new Appointment("Rupert", "Amodia", DateTime.Parse("2022-12-01 09:00:00"), "Dr. Amr, GP"),
                new Appointment("Rupert", "Amodia", DateTime.Parse("2022-12-01 09:30:00"), "Dr. Amr, GP"),
                new Appointment("Amy", "Santiago", DateTime.Parse("2022-12-01 12:00:00"), "Dr. Amr, GP"),
                new Appointment("Charles", "Boyle", DateTime.Parse("2022-10-31 14:00:00"), "Dr. Amr, GP"),
                new Appointment("Rosa", "Diaz", DateTime.Parse("2022-11-07 09:00:00"), "Dr. Amr, GP"),
                new Appointment("Rosa", "Diaz", DateTime.Parse("2022-11-07 09:30:00"), "Dr. Amr, GP"),
                new Appointment("Raymond", "Holt", DateTime.Parse("2022-11-07 11:30:00"), "Dr. Amr, GP"),
                new Appointment("Kevin", "Cozner", DateTime.Parse("2022-11-07 13:30:00"), "Dr. Amr, GP"),
                new Appointment("Araiz", "Asad", DateTime.Parse("2022-11-07 09:00:00"), "Dr. Raphael, GP"),
                new Appointment("Elizabeth Chu", "Asad", DateTime.Parse("2022-11-08 15:00:00"), "Dr. Amr, GP"),
                new Appointment("David", "Smith", DateTime.Parse("2022-11-02 11:30:00"), "Dr. Raphael, GP")
            };

            return Appointments.Where(appointment => appointment.StartDate.Date == quickViewDate.Date)
                .OrderBy(appointment => appointment.StartDate)
                .ToList();
        }
    }


}
