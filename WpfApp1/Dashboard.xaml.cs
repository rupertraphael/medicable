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
        private int quickViewPage;
        private List<Appointment>[] Appointments;
        private string[] dates = {
                "December 1", "December 2", "December 3" , "December 4", "December 5"
            };

        public Dashboard()
        {
            InitializeComponent();

            this.quickViewPage = 0;

            quickViewTitle.Text = this.dates[quickViewPage];

            this.Appointments = new List<Appointment>[5];

            List<Appointment> today = new List<Appointment>();
            today.Add(new Appointment("Rupert", "Amodia", "9:00AM", "9:30AM", "Dr. Chirag"));
            today.Add(new Appointment("Araiz", "Asad", "10:00am", "10:30am", "Dr. Raphael"));
            today.Add(new Appointment("elizabeth chu", "asad", "11:00am", "10:30am", "dr. amr"));
            today.Add(new Appointment("david", "smith", "11:00am", "11:30am", "dr. raphael"));
            today.Add(new Appointment("john", "cena", "12:30pm", "1:00pm", "dr. raphael"));
            today.Add(new Appointment("matthew", "murdock", "1:00pm", "1:30pm", "dr. amr"));
            today.Add(new Appointment("jennifer", "walters", "1:00pm", "1:30pm", "dr. chirag"));
            today.Add(new Appointment("muhammad", "mohammed", "2:00pm", "2:30pm", "dr. chirag"));

            this.Appointments[0] = today;
            this.Appointments[1] = new List<Appointment>();
            quickView.ItemsSource = this.Appointments[quickViewPage];
        }

        private void Book_Existing_Patient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Patients.xaml", UriKind.Relative), "patients");
        }

        private void Quickview_Next(object sender, RoutedEventArgs e)
        {
            this.quickViewPage = (this.quickViewPage + 1) % this.Appointments.Length;
            this.setQuickViewBasedOnPage(this.quickViewPage);
        }

        private void Quickview_Previous(object sender, RoutedEventArgs e)
        {
            if (this.quickViewPage == 0)
            {
                this.quickViewPage = this.Appointments.Length;
            }

            this.quickViewPage = this.quickViewPage - 1;
            this.setQuickViewBasedOnPage(this.quickViewPage);
        }

        private void setQuickViewBasedOnPage(int page)
        {
            quickView.ItemsSource = this.Appointments[page];
            quickViewTitle.Text = this.dates[page];
            if(page == 0)
            {
                quickViewSubtitle.Text = "Appointments for Today";
            } else
            {
                quickViewSubtitle.Text = "Appointments for ";
            }
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            string str = (string)e.ExtraData;

            Trace.WriteLine("navigated to dashboard!");

            // do whatever with str, like assign to a view model field, etc.
        }

        private void quickView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


}
