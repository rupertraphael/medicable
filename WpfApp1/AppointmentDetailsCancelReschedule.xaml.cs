using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AppointmentDetailsCancelReschedule.xaml
    /// </summary>
    public partial class AppointmentDetailsCancelReschedule : Page
    {
        Appointment selectedAppointment;
        APatient patient;
        public AppointmentDetailsCancelReschedule(APatient patient, Appointment appointment)
        {
            InitializeComponent();

            selectedAppointment = appointment;
            this.patient = patient;

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

            doctorlist.SelectedValue = selectedAppointment.Doctor.DisplayName;
            doctorlist.IsEnabled = false;

            reason.SelectedValue = selectedAppointment.Reason;
            reason.IsEnabled = false;

            notes.Text = selectedAppointment.Notes;
            notes.IsEnabled = false;
        }
        
        public void selectDoctor(string doctor)
        {
            doctorlist.SelectedValue = doctor;
        }

        public void selectDate(DateTime date)
        {
            datepicker.SelectedDate = date;
            selectedDateTimes.Add(date);
        }

        private List<DateTime> selectedDateTimes = new List<DateTime>();
        public void clearSelectedDateTimes()
        {
            selectedDateTimes.Clear();
        }

        public void selectTime(DateTime dt)
        {
            KeyValuePair<DateTime, string> item = new KeyValuePair<DateTime, string>(
                        dt,
                        dt.ToString("t") + " - " + dt.AddMinutes(30).ToString("t")
                    );

            timepicker.SelectedItems.Add(item);
        }

        public void selectTime(KeyValuePair<DateTime, string> dt)
        {
            var adjacent = selectedDateTimes.Where(i => dt.Key.Equals(i.AddMinutes(30)) || dt.Key.Equals(i.AddMinutes(-30))).ToList();

            if (selectedDateTimes.Count == 0 || adjacent.Count > 0)
            {
                selectedDateTimes.Add(dt.Key);
            }
            else
            {
                selectedDateTimes = new List<DateTime>()
                {
                    dt.Key
                };

                timepicker.UnselectAll();
                timepicker.SelectedItems.Add(dt);
            }
        }
        public void unselectTime(KeyValuePair<DateTime, string> dt)
        {
            selectedDateTimes.Remove(dt.Key);
        }

        private void timepicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<int> indices = new List<int>();

            if (e.AddedItems.Count > 0)
            {
                if (timepicker.Items.IndexOf(e.AddedItems[0]) == 0)
                {
                    clearSelectedDateTimes();
                    timepicker.UnselectAll();
                    if(timepicker.SelectedItems.Count == 0)
                    {
                        timepicker.SelectedItems.Add(e.AddedItems[0]);
                    }
                    
                    BookButton.Content = "Proceed to Calendar";

                    return;
                }

                selectTime((KeyValuePair<DateTime, string>)e.AddedItems[0]);
            }
            else
            {
                if (timepicker.Items.IndexOf(e.RemovedItems[0]) != 0)
                    unselectTime((KeyValuePair<DateTime, string>)e.RemovedItems[0]);
            }


            if (timepicker.SelectedItems.Count > 0)
            {
                BookButton.Content = "Reschedule";
            }
            if (timepicker.SelectedItems.Count == 0)
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
                    if (timepicker.SelectedItems.Count > 0)
                    {
                        var item = DB.Appointments.RemoveAll(x => x.StartDate == selectedAppointment.StartDate && x.Patient.PatientHealthCareNumber == patient.PatientHealthCareNumber);
                        foreach (DateTime slot in selectedDateTimes)
                        {
                            DB.Appointments.Add(
                                new Appointment(
                                    patient,
                                    slot,
                                    DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue),
                                    (string)reason.SelectedValue,
                                    notes.Text));
                        }

                        string messageBoxText = "Appointment Rescheduled!";
                        string caption = "Appointment Rescheduled";
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
                        GoToCalendar();
                    }
                }
                else if (reason.SelectedIndex == 4)
                {
                    if (notes.Text != "")
                    {
                        if (timepicker.SelectedItems.Count > 0)
                        {
                            var item = DB.Appointments.RemoveAll(x => x.StartDate == selectedAppointment.StartDate && x.Patient.PatientHealthCareNumber == patient.PatientHealthCareNumber);
                            foreach (DateTime slot in selectedDateTimes)
                            {
                                DB.Appointments.Add(
                                    new Appointment(
                                        patient,
                                        slot,
                                        DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue),
                                        (string)reason.SelectedValue,
                                        notes.Text));
                            }

                            string messageBoxText = "Appointment Rescheduled!";
                            string caption = "Appointment Rescheduled";
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
                            GoToCalendar();
                        }
                    }
                    else
                    {
                        Noteserror.Visibility = Visibility.Visible;
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
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.GoBack();
        }

        private void GoToCalendar()
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            Calendar page;

            if ((string)doctorlist.SelectedValue == "" || (DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue)) == null)
            {
                page = new Calendar();
            }
            else
            {
                page = new Calendar((DB.Doctors.Find(d => d.DisplayName == (string)doctorlist.SelectedValue)));
            }

            if (datepicker.SelectedDate.HasValue)
            {
                page.GoToDate((DateTime)datepicker.SelectedDate);
                Trace.WriteLine(datepicker.SelectedDate);
            }

            page.reschedule = true;

            page.appointmentDetailsCancelRescheduleNext = this;
            page.appointmentDetailsCancelReschedulePrevious = this;

            ns.Navigate(page);
        }

        public void populateTime()
        {
            timepicker.Items.Clear();


            ListBoxItem blank = new ListBoxItem();
            blank.Content = "Click this to clear selection";
            timepicker.Items.Add(blank);

            List<Appointment> appointments = DB.Appointments.Where(a => a.StartDate.Date == datepicker.SelectedDate).ToList();

            if (datepicker.SelectedDate == null)
            {
                return;
            }

            timepicker.IsEnabled = true;

            DateTime start = ((DateTime)datepicker.SelectedDate).AddHours(9);
            DateTime end = ((DateTime)datepicker.SelectedDate).AddHours(16);

            List<DateTime> slots = new List<DateTime>();

            while (start <= end)
            {
                if (appointments.Where(a => a.StartDate == start && a.Doctor.DisplayName == (string)doctorlist.SelectedValue).Count() == 0)
                {
                    slots.Add(start);
                }



                start = start.AddMinutes(30);
            }

            foreach (DateTime slot in slots)
            {
                timepicker.Items.Add(
                    new KeyValuePair<DateTime, string>(
                        slot,
                        slot.ToString("t") + " - " + slot.AddMinutes(30).ToString("t")
                    ));
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Are you sure you want to cancel this appointment?";
            string caption = "Cancel Appointment";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                var item = DB.Appointments.RemoveAll(x => x.StartDate == selectedAppointment.StartDate && x.Patient.PatientHealthCareNumber == patient.PatientHealthCareNumber);
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new Dashboard());
            }
        }
    }
}
