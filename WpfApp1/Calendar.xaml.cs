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
    public class Appointment
    {
        private string _firstName;
        private string _lastName;
        private string _doctor;
        private DateTime _startDate;


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

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public string FullName
        {
            get { return _firstName + " " + _lastName; }
        }

        public string Doctor
        {
            get { return _doctor; }
        }

        public Appointment(
            string firstname,
            string lastname,
            DateTime startDate,
            string doctor)
        {
            _firstName = firstname;
            _lastName = lastname;
            _startDate = startDate;
            _doctor = doctor;
        }
    }

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
        private int page = 2;

        public string preSelectedDoctor = "Dr. Amr, GP";

        public Calendar()
        {
            // initialize days being shown on the calendar

            calendarDays.Add(31); // October 31
            calendarDays.AddRange(Enumerable.Range(1, 30)); // November.. 1-30
            calendarDays.AddRange(Enumerable.Range(1, 31)); // December.. 31-61
            calendarDays.AddRange(Enumerable.Range(1, 31)); // January.. 62-92
            calendarDays.AddRange(Enumerable.Range(1, 5)); // February.. 93-97

            InitializeComponent();

            SelectedDoctor.SelectedValue = preSelectedDoctor;

            renderCalendarPage();
        }

        private void renderCalendarPage()
        {
            CalendarRows.RowDefinitions.Clear();
            CalendarRows.Children.Clear();

            SelectedMonth.SelectedValue = getMonthByPage();

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

                        appointmentButton.Background = Brushes.Transparent;
                        appointmentButton.BorderThickness = new Thickness(0);

                        RowDefinition appointmentCellRD = new RowDefinition();
                        appointmentCell.RowDefinitions.Add(appointmentCellRD);

                        appointmentCell.Children.Add(appointmentButton);

                        appointmentButton.Checked += (sender, e) => AppointmentButton_Checked(sender, e, calendarStartDateTime, appointmentButton);
                        appointmentButton.IsChecked = selectedDateTimes.ContainsKey(calendarStartDateTime);
                    }

                    appointmentCellContainer.Child = appointmentCell;
                    columns.Children.Add(appointmentCellContainer);
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
        private void AppointmentButton_Checked(object sender, RoutedEventArgs e, DateTime dt, CheckBox cb)
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

                TextBlock dateTB = new TextBlock();
                dateTB.Text = getDateForCalendarColumn(i).ToString();
                dateTB.FontWeight = FontWeights.Bold;

                dayWP.Children.Add(dayTB);
                dayWP.Children.Add(dateTB);

                Border dateContainer = new Border();
                dateContainer.BorderThickness = new Thickness(0, 0, 0, 1);
                dateContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                dateContainer.Padding = new Thickness(2);
                dateContainer.Child = dayWP;
                dateContainer.SetValue(Grid.ColumnProperty, i + 1);

                CalendarDaysRow.Children.Add(dateContainer);
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

        private void setPageByDoctor(object sender, EventArgs e)
        {
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
            List<Appointment> Appointments = new List<Appointment>
            {
                new Appointment("Rupert", "Amodia", DateTime.Parse("2022-10-31 09:00:00"), "Dr. Amr, GP"),
                new Appointment("Jacob", "Peralta", DateTime.Parse("2022-10-31 10:00:00"), "Dr. Amr, GP"),
                new Appointment("Amy", "Santiago", DateTime.Parse("2022-10-31 12:00:00"), "Dr. Amr, GP"),
                new Appointment("Charles", "Boyle", DateTime.Parse("2022-10-31 14:00:00"), "Dr. Amr, GP"),
                new Appointment("Rosa", "Diaz", DateTime.Parse("2022-11-07 09:00:00"), "Dr. Amr, GP"),
                new Appointment("Rosa", "Diaz", DateTime.Parse("2022-11-07 09:30:00"), "Dr. Amr, GP"),
                new Appointment("Raymond", "Holt", DateTime.Parse("2022-11-07 11:30:00"), "Dr. Amr, GP"),
                new Appointment("Kevin", "Cozner", DateTime.Parse("2022-11-07 13:30:00"), "Dr. Amr, GP"),
                new Appointment("Araiz", "Asad", DateTime.Parse("2022-11-07 09:00:00"), "Dr. Raphael, GP"),
                new Appointment("Elizabeth Chu", "Asad", DateTime.Parse("2022-11-08 15:00:00"), "Dr. Amr, GP"),
                new Appointment("David", "Smith", DateTime.Parse("2022-11-02 11:30:00"), "Dr. Raphael, GP")
            };

            Appointments = Appointments.Where(a => a.Doctor == (string) SelectedDoctor.SelectedValue).ToList();

            return Appointments.ToDictionary(appointment => appointment.StartDate, appointment => appointment);
        }

        public bool datesSelected = false;
        public bool enabled
        {
            get { return datesSelected; }
            set
            {
                datesSelected = value;
            }
        }
    }
}
