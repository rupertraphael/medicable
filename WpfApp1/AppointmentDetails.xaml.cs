using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Schema;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AppointmentDetails.xaml
    /// </summary>
    public partial class AppointmentDetails : Page
    {
        public AppointmentDetails(APatient patient)
        {
            InitializeComponent();

            patientname.Text = patient.PatientName;
            healthcareid.Text = patient.PatientHealthCareNumber;
            phonenumber.Text = patient.PatientPhoneNumber;

            // insert doctors to combo box
            foreach (Doctor doctor in DB.Doctors.OrderBy(doctor => doctor.Name).ToList())
            {
                ComboBoxItem doctorContainer = new ComboBoxItem();
                switch (doctor.Specialization)
                {
                    case "GP":
                        doctorContainer.Background = Brushes.LightBlue;
                        break;
                    case "Dermatologist":
                        doctorContainer.Background = Brushes.LightGoldenrodYellow;
                        break;
                    case "Chiropractor":
                        doctorContainer.Background = Brushes.LightPink;
                        break;
                    default:
                        doctorContainer.Background = Brushes.Transparent;
                        break;
                }
                doctorContainer.Content = doctor.DisplayName;
                doctorlist.Items.Add(doctorContainer);
            }

            if (patient.PatientPreferredDoctor != "")
            {
                Trace.WriteLine(patient.PatientPreferredDoctor);
                doctorlist.SelectedValue = patient.PatientPreferredDoctor;

            }
        }

        private void timepicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (timepicker.SelectedIndex > 0)
            {
                BookButton.Content = "Book Appointment";
            }
            if (timepicker.SelectedIndex == 0)
            {
                BookButton.Content = "Proceed to Calendar";
            }
        }

        private void reason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reason.SelectedIndex > -1)
            {
                if (datepicker.SelectedDate.HasValue)
                {
                    timepicker.IsEnabled = true;
                    populateTime();
                }
                Reasonerror.Visibility = Visibility.Collapsed;
            }

        }

        private void datepicker_DateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reason.SelectedIndex > -1)
            {
                if (datepicker.SelectedDate.HasValue)
                {
                    timepicker.IsEnabled = true;
                    populateTime();
                }
                Reasonerror.Visibility = Visibility.Collapsed;
            }

        }

        /*|       private void datepicker_CalendarOpened(object sender, RoutedEventArgs e)
                {
                    var calendar = datepicker.Template.FindName("PART_Calendar", datepicker) as Calendar;
                    var clearButton = new Button { Content = "Clear" };
                    clearButton.Click += OnClearButtonClick;
                    calendar.Children.Add(clearButton);
                }*/

        /*        private void OnClearButtonClick(object sender, RoutedEventArgs e)
                {
                    datepicker.SelectedDate = null;
                }*/

        private void doctorlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reason.SelectedIndex > -1)
            {
                if (datepicker.SelectedDate.HasValue)
                {
                    timepicker.IsEnabled = true;
                    populateTime();
                }
                Reasonerror.Visibility = Visibility.Collapsed;
            }
        }
        
        private void notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            Noteserror.Visibility = Visibility.Collapsed;  
        }
        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if (reason.SelectedIndex > -1)
            {
                if (reason.SelectedIndex < 4)
                {
                    if (timepicker.SelectedIndex > 0)
                    {
                        string messageBoxText = "Appointment Confirmed!";
                        string caption = "Appointment Confirmation";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Information;
                        MessageBoxResult result;

                        result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                        if (result == MessageBoxResult.OK)
                        {
                            NavigationService ns = NavigationService.GetNavigationService(this);
                            ns.Navigate(new Dashboard());
                        }


                    }
                    else
                    {
                        NavigationService ns = NavigationService.GetNavigationService(this);
                        if ((string)doctorlist.SelectedValue == "" || (DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue)) == null)
                        {
                            ns.Navigate(new Calendar());
                        }
                        else
                        {
                            ns.Navigate(new Calendar((DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue))));
                        }
                    }
                }
                else if (reason.SelectedIndex == 4)
                {
                    if(notes.Text != "")
                    {
                        if (timepicker.SelectedIndex > 0)
                        {
                            string messageBoxText = "Appointment Confirmed!";
                            string caption = "Appointment Confirmation";
                            MessageBoxButton button = MessageBoxButton.OK;
                            MessageBoxImage icon = MessageBoxImage.Information;
                            MessageBoxResult result;

                            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                            if (result == MessageBoxResult.OK)
                            {
                                NavigationService ns = NavigationService.GetNavigationService(this);
                                ns.Navigate(new Dashboard());
                            }


                        }
                        else
                        {
                            NavigationService ns = NavigationService.GetNavigationService(this);
                            if ((string)doctorlist.SelectedValue == "" || (DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue)) == null)
                            {
                                ns.Navigate(new Calendar());    
                            }
                            else
                            {
                                ns.Navigate(new Calendar((DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue))));
                            }
                        }
                    }
                    else
                    {
                        Noteserror.Visibility= Visibility.Visible;
                    }
                }
            }
            else
            {
                Reasonerror.Visibility = Visibility.Visible;
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {

        }

       /* private void GoToCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);

            if ((string)doctorlist.SelectedValue == "" || (DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue)) == null)
            {
                ns.Navigate(new Calendar());
            } else
            {
                ns.Navigate(new Calendar((DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue))));
            }
        }*/

        private void populateTime()
        {
            timepicker.Items.Clear();


            ComboBoxItem blank = new ComboBoxItem();
            blank.Content = "";
            timepicker.Items.Add(blank);

            List <Appointment> appointments = DB.Appointments.Where(a => a.StartDate.Date == datepicker.SelectedDate).ToList();

            if(datepicker.SelectedDate == null) {
                Trace.WriteLine("cant do tanads");
                return;
            }

            timepicker.IsEnabled = true;
            
            DateTime start = ((DateTime) datepicker.SelectedDate).AddHours(9);
            DateTime end = ((DateTime)datepicker.SelectedDate).AddHours(16);

            List<DateTime> slots = new List<DateTime>();

            while (start <= end)
            {
                if(appointments.Where(a => a.StartDate == start && a.Doctor.DisplayName == (string)doctorlist.SelectedValue).Count() == 0)
                {
                    slots.Add(start);
                    Trace.WriteLine(start.ToString("f"));
                }

                

                start = start.AddMinutes(30);
            }

            foreach (DateTime slot in slots)
            {
                ComboBoxItem timeContainer = new ComboBoxItem();

                timeContainer.Content = slot.ToString("t") + " - " + slot.AddMinutes(30).ToString("t");
                timepicker.Items.Add(timeContainer);
            }

            
        }
    }
}

// did not include bordercolor change for reason combobox for errors
// did not include a clear button for the datepicker 
// did not connect functionalities -- autofilling patient name, preferred doctor, AND setting doctor, date -> calendar
// rescheduling, followup, cancel appointment
// screen size adjust
