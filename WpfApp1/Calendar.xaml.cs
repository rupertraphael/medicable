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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        private List<int> calendarDays = new List<int>();
        private List<string> Days = new List<string>()
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };

        private List<DateTime> grayedOutDates = new List<DateTime>()
        {
            DateTime.Parse("2022-10-31 09:00:00"),
            DateTime.Parse("2022-11-28 09:00:00"),
            DateTime.Parse("2022-11-29 09:00:00"),
            DateTime.Parse("2022-11-30 09:00:00"),
            DateTime.Parse("2023-01-01 09:00:00"),
            DateTime.Parse("2023-02-01 09:00:00"),
            DateTime.Parse("2023-02-02 09:00:00"),
            DateTime.Parse("2023-02-03 09:00:00"),
            DateTime.Parse("2023-02-04 09:00:00"),
            DateTime.Parse("2023-02-05 09:00:00")
        };

        private List<int> grayedOutDates2 = new List<int>()
        {
            0,28,29,30,62,93,94,95,96,97
        };
        private int page = 5;

        private int today = 31;

        private string preSelectedDoctor = (DB.Doctors.OrderBy(d => d.Name).ToList())[0].DisplayName;

        public Calendar(Doctor selectedDoctor) : this()
        {
            SelectedDoctor.SelectedValue = selectedDoctor.DisplayName;
        }
        public Calendar()
        {
            // initialize days being shown on the calendar

            calendarDays.Add(31); // October 31
            calendarDays.AddRange(Enumerable.Range(1, 30)); // November.. 1-30
            calendarDays.AddRange(Enumerable.Range(1, 31)); // December.. 31-61
            calendarDays.AddRange(Enumerable.Range(1, 31)); // January.. 62-92
            calendarDays.AddRange(Enumerable.Range(1, 5)); // February.. 93-97

            InitializeComponent();

            renderCalendarPage();

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
                SelectedDoctor.Items.Add(doctorContainer);
            }

            SelectedDoctor.SelectedValue = preSelectedDoctor;
        }

        private void renderCalendarPage()
        {
            CalendarRows.RowDefinitions.Clear();
            CalendarRows.Children.Clear();

            SelectedMonth.SelectedValue = getMonthByPage();

            // only the time is important here and not the date.
            var startTime = DateTime.Parse("2012-01-28 09:00:00");
            
            var endTime = startTime.AddHours(7);

            int row = 0;
            while (startTime <= endTime)
            {
                // rowContainer is the wrapper for every calendar row
                // which corresponds to 1 hour in the calendar
                Border rowContainer = new Border();

                // tb is the hour (e.g. 3PM) textblock
                TextBlock tb = new TextBlock();
                tb.Text = startTime.ToShortTimeString();
                tb.TextAlignment = TextAlignment.Right;

                rowContainer.SetValue(Grid.RowProperty, row++);
                rowContainer.BorderThickness = new Thickness(0, 0, 0, 1);
                rowContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;

                // set the columns - sections where to put the appointments in
                // according to date and time
                // days will be from Mon-Sat
                Grid columns = new Grid();
                // cd for hour
                ColumnDefinition hcd = new ColumnDefinition();
                GridLengthConverter glc = new GridLengthConverter();
                hcd.Width = (GridLength)glc.ConvertFromString("0.5*");

                Border hourCellContainer = new Border();
                hourCellContainer.Child = tb;
                hourCellContainer.BorderThickness = new Thickness(0, 0, 1, 0);
                hourCellContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                hourCellContainer.Padding = new Thickness(2);

                columns.ColumnDefinitions.Add(hcd);
                columns.Children.Add(hourCellContainer);

                for (int i = 0; i < Days.Count; i++)
                {
                    DateTime calendarStartDateTime = DateTime.Parse(
                        String.Format(
                                "{0} {1}", getDate(getCalendarDaysIndexByColumn(i)), startTime.ToLongTimeString()
                            )
                        );

                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = (GridLength)glc.ConvertFromString("1*");
                    columns.ColumnDefinitions.Add(cd);

                    Grid appointmentCell = new Grid();
                    Border appointmentCellContainer = new Border();
                    appointmentCellContainer.BorderThickness = new Thickness(0, 0, 1, 0);
                    appointmentCellContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                    appointmentCellContainer.SetValue(Grid.ColumnProperty, i + 1);

                    if (hasAppointment(calendarStartDateTime))
                    {
                        // render Appointment

                        Appointment appointment = this.getAppointments()[calendarStartDateTime];

                        Border b = new Border();
                        b.Background = (new BrushConverter()).ConvertFromString("#dbeafe") as Brush;
                        b.Padding = new Thickness(4);
                        b.SetValue(Grid.RowProperty, 0);
                        b.Margin = new Thickness(2, 1, 2, 1);

                        TextBlock atb = new TextBlock();
                        atb.Text = "Appointment with " + appointment.FullName;
                        atb.TextWrapping = TextWrapping.Wrap;
                        atb.VerticalAlignment = VerticalAlignment.Center;
                        atb.Foreground = (new BrushConverter()).ConvertFromString("#1e3a8a") as Brush;
                        atb.TextTrimming = TextTrimming.CharacterEllipsis;

                        b.Child = atb;

                        RowDefinition appointmentCellRD = new RowDefinition();
                        appointmentCellRD.Height = (GridLength)glc.ConvertFromString("1*");
                        appointmentCell.RowDefinitions.Add(appointmentCellRD);

                        appointmentCell.Children.Add(b);
                    }
                    else
                    {
                        // render vacant appointment

                        CheckBox appointmentButton = new CheckBox();
                        //ScaleTransform scale = new ScaleTransform(2.0, 2.0);
                        //appointmentButton.RenderTransformOrigin = new Point(0.5, 0.5);
                        //appointmentButton.RenderTransform = scale;

                        appointmentButton.IsEnabled = getCalendarDaysIndexByColumn(i) >= today;

                        appointmentButton.Background = Brushes.Transparent;
                        appointmentButton.BorderThickness = new Thickness(0);

                        RowDefinition appointmentCellRD = new RowDefinition();
                        appointmentCell.RowDefinitions.Add(appointmentCellRD);

                        appointmentCell.Children.Add(appointmentButton);

                        appointmentButton.Foreground = Brushes.Transparent;
                        appointmentButton.Checked += (sender, e) => AppointmentButton_Checked(sender, e, calendarStartDateTime, appointmentButton, appointmentCellContainer);
                        appointmentButton.Unchecked += (sender, e) => AppointmentButton_Unchecked(sender, e, calendarStartDateTime, appointmentButton, appointmentCellContainer);
                        appointmentButton.IsChecked = selectedDateTimes.ContainsKey(calendarStartDateTime);
                    }

                    appointmentCellContainer.Child = appointmentCell;
                    columns.Children.Add(appointmentCellContainer);

                    if (grayedOutDates.Exists(date => date.Date.Equals(calendarStartDateTime.Date)))
                    {
                        appointmentCellContainer.Background = (new BrushConverter()).ConvertFromString("#e5e7eb") as Brush;
                    }
                }

                rowContainer.Child = columns;

                // set RD for every calendar row
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(50);
                CalendarRows.RowDefinitions.Add(rd);

                CalendarRows.Children.Add(rowContainer);

                startTime = startTime.AddMinutes(30);
            }

            renderCalendarDayRow();
        }

        private Dictionary<DateTime, CheckBox> selectedDateTimes= new Dictionary<DateTime, CheckBox>();

        private void clearSelectedDateTimes()
        {
            selectedDateTimes.Clear();
        }
        private void AppointmentButton_Checked(object sender, RoutedEventArgs e, DateTime dt, CheckBox cb, Border container)
        {
            var adjacent = selectedDateTimes.Where(i => dt.Equals(i.Key.AddMinutes(30)) || dt.Equals(i.Key.AddMinutes(-30))).ToDictionary(p => p.Key, p => p.Value);

            if (selectedDateTimes.Count == 0 || adjacent.Count > 0)
            {
                selectedDateTimes[dt] = cb;
            }
            else
            {
                foreach (KeyValuePair<DateTime, CheckBox> sdt in selectedDateTimes)
                {
                    sdt.Value.IsChecked = false;
                }

                selectedDateTimes = new Dictionary<DateTime, CheckBox>()
                {
                    {dt, cb}
                };
            }

            container.Background = (new BrushConverter()).ConvertFromString("#bbf7d0") as Brush;
            enableSelectAppointment();
            disableSkip();
            setSelectedAppointmentText();
        }
        private void AppointmentButton_Unchecked(object sender, RoutedEventArgs e, DateTime dt, CheckBox cb, Border container)
        {
            selectedDateTimes.Remove(dt);
            if (grayedOutDates.Exists(date => date.Date.Equals(dt.Date)))
            {
                container.Background = (new BrushConverter()).ConvertFromString("#e5e7eb") as Brush;
            } else
            {
                container.Background = Brushes.Transparent;
            }

            if (selectedDateTimes.Count == 0)
            {
                enableSkip();
                disableSelectAppointment();
            }

            setSelectedAppointmentText();
        }

        private void setSelectedAppointmentText()
        {
            List <DateTime> times = selectedDateTimes.Keys.ToList().OrderBy(dt => dt).ToList();

            if(times.Count == 0) 
            {
                SelectedAppointmentText.Text = "None";
                return;
            }

            DateTime first = times.First();
            DateTime last = times.Last();

            SelectedAppointmentText.Text = times.First().ToString("f") + " - " + times.Last().AddMinutes(30).ToString("t");
        }


        private void renderCalendarDayRow()
        {
            CalendarDaysRow.Children.Clear();

            Border c = new Border();
            c.BorderThickness = new Thickness(0, 0, 0, 1);
            c.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
            c.Padding = new Thickness(2);
            c.SetValue(Grid.ColumnProperty, 0);
            CalendarDaysRow.Children.Add(c);


            for (int i = 0; i < Days.Count; i++)
            {
                WrapPanel dayWP = new WrapPanel();
                dayWP.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock dayTB = new TextBlock();
                dayTB.Text = this.Days[i] + " ";
                dayTB.Padding = new Thickness(2);

                TextBlock dateTB = new TextBlock();
                dateTB.Text = getDateForCalendarColumn(i).ToString();
                dateTB.FontWeight = FontWeights.Bold;
                dateTB.Padding = new Thickness(2);

                dayWP.Children.Add(dayTB);
                dayWP.Children.Add(dateTB);

                Border dateContainer = new Border();
                dateContainer.BorderThickness = new Thickness(0, 0, 0, 1);
                dateContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                dateContainer.Padding = new Thickness(2);
                dateContainer.Child = dayWP;
                dateContainer.SetValue(Grid.ColumnProperty, i + 1);

                CalendarDaysRow.Children.Add(dateContainer);

                // highlight today
                if(getCalendarDaysIndexByColumn(i) == today)
                {
                    dateTB.Background = (new BrushConverter()).ConvertFromString("#ef4444") as Brush;
                    dateTB.Foreground = (new BrushConverter()).ConvertFromString("#fee2e2") as Brush;
                    dayTB.Foreground = (new BrushConverter()).ConvertFromString("#fee2e2") as Brush;
                    dayTB.Background = (new BrushConverter()).ConvertFromString("#ef4444") as Brush;
                }

            }

        }

        private int getCalendarDaysIndexByColumn(int column)
        {
            return (this.page - 1) * Days.Count + column;
        }

        private int getDateForCalendarColumn(int column)
        {
            return this.calendarDays[getCalendarDaysIndexByColumn(column)];
        }

        private string getDate(int calendarDaysIndex)
        {
            string year = "2022";
            string month = "10";

            if (calendarDaysIndex > 92)
            {
                year = "2023";
                month = "02";
            }
            else if (calendarDaysIndex > 61) {
                year = "2023";
                month = "01";
            }
            else if (calendarDaysIndex > 30) {
                year = "2022";
                month = "12";
            }
            else if (calendarDaysIndex > 0)
            {
                year = "2022";
                month = "11";
            }

            string date = calendarDays[calendarDaysIndex].ToString();

            return String.Format("{0}-{1}-{2}", year, month, date);
        }

        private void NextWeek(object sender, RoutedEventArgs e)
        {
            this.page++;

            if (this.page > this.getMaxPage())
            {
                this.page = this.getMaxPage();
            }

            renderCalendarPage();
            SelectedMonth.SelectedValue = getMonthByPage();
        }

        private void PreviousWeek(object sender, RoutedEventArgs e)
        {
            this.page--;

            if (this.page < 1)
            {
                this.page = 1;
            }

            renderCalendarPage();
            SelectedMonth.SelectedValue = getMonthByPage();
        }

        private int getMaxPage()
        {
            return calendarDays.Count / Days.Count;
        }

        private string getMonthByPage()
        {
            if (page > 9)
                return "January 2023"; // January

            if (page > 4)
                return "December 2022"; // December

            return "November 2022";
        }

        private void setPageByMonth(object sender, EventArgs e)
        {
            switch (SelectedMonth.SelectedValue)
            {
                case "December 2022":
                    this.page = 5;
                    break;
                case "January 2023":
                    this.page = 10;
                    break;
                default:
                    this.page = 1;
                    break;
            }

            renderCalendarPage();
        }


        private bool handleDoctorChange = true;
        private void setPageByDoctor(object sender, SelectionChangedEventArgs e)
        {
            // not so sure about this..
            // is slower (+1 click) for people experienced with the system
            // but might accomodate people who are still new to it
            // might not want to actually add this in because the consequences of clearing out the selected appointments is not that bad
            // user can simply click the appointments again and if they've accidentally cleared the selection, 
            // it is likely that the patient must've already mentioned they want those slots
            
            // but is a good topic
            // to discuss in the report or evaluation?
            if(handleDoctorChange)
            {
                if (selectedDateTimes.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "You have already selected appointments. Do you want to change the selected doctor and clear the selected appointments?",
                        "Change Doctor",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No)
                    {
                        // https://stackoverflow.com/a/8843675
                        ComboBox combo = (ComboBox)sender;
                        handleDoctorChange = false;
                        combo.SelectedItem = e.RemovedItems[0];
                        return;
                    } else
                    {
                        clearSelectedDateTimes();
                    }
                }
            }
            handleDoctorChange = true;

            
            renderCalendarPage();
        }

        private Boolean hasAppointment(DateTime dt)
        {
            Dictionary<DateTime, Appointment> appointments = getAppointments();

            if (appointments.Count == 0 )
                return false;

            return appointments.ContainsKey(dt);
        }

        private Dictionary<DateTime, Appointment> getAppointments()
        {
            List<Appointment> Appointments = DB.Appointments;

            Appointments = Appointments.Where(a => a.Doctor.DisplayName == (string) SelectedDoctor.SelectedValue).ToList();

            return Appointments.ToDictionary(appointment => appointment.StartDate, appointment => appointment);
        }

        private void disableSkip()
        {
            SkipButton.IsEnabled = false;
            SkipButtonBorder.BorderBrush = Brushes.LightGray;
        }

        private void enableSkip()
        {
            SkipButton.IsEnabled = true;
            SkipButtonBorder.BorderBrush = (new BrushConverter()).ConvertFromString("#f9fafb") as Brush;
        }

        private void disableSelectAppointment ()
        {
            SelectAppointmentButton.IsEnabled = false;
            SelectAppointmentButtonBorder.BorderBrush = Brushes.LightGray;
        }

        private void enableSelectAppointment()
        {
            SelectAppointmentButton.IsEnabled = true;
            SelectAppointmentButtonBorder.BorderBrush = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
        }

        public AppointmentDetails appointmentDetailsPrevious { get; set; }
        public AppointmentDetails appointmentDetailsNext { get; set; }
        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            // Immediately skip. Don't need to confirm with user since
            // they haven't really selected appointments. Consequence
            // to accidentally pressing skip is not as bad as accidentally pressing
            // back when appointments are already selected.
           
            goToSetAppointmentDetails(appointmentDetailsPrevious);
        }

        private void goToSetAppointmentDetails(AppointmentDetails page)
        {
            if (page == null)
            {
                throw  new NullReferenceException("You have not instantiated the given page.");
            }

            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            // We're confirming with user if they want to go back after already selecting appointments.
            if (selectedDateTimes.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show(
                    "You have already selected appointments. Do you want to clear the selected appointments and go back?",
                    "Go Back to Setting Appointment Details",
                    MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    clearSelectedDateTimes();
                }
            }

            // appointmentDetailsPrevious must be set.
            goToSetAppointmentDetails(appointmentDetailsPrevious);
        }

        private void SelectAppointmentSlot_Click(object sender, RoutedEventArgs e)
        {
            // TODO: maybe set the appointment details page with new data (i.e. selected doctor, date, slots).

            goToSetAppointmentDetails(appointmentDetailsNext);
        }

        private void GoToToday(object sender, RoutedEventArgs e)
        {
            this.page = 5;

            renderCalendarPage();
            SelectedMonth.SelectedValue = getMonthByPage();
        }
    }
}
