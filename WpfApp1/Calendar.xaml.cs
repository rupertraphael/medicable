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

        List<int> calendarDays = new List<int>();
        List<string> Days = new List<string>()
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };
        private int page = 2;

        public Calendar()
        {
            // initialize days being shown on the calendar
            
            calendarDays.Add(31); // October 31
            calendarDays.AddRange(Enumerable.Range(1, 30)); // November
            calendarDays.AddRange(Enumerable.Range(1, 31)); // December
            calendarDays.AddRange(Enumerable.Range(1, 31)); // January
            calendarDays.AddRange(Enumerable.Range(1, 5)); // February


            List<string> hours = new List<string>();
            hours.Add("9AM");
            hours.Add("10AM");
            hours.Add("11AM");
            hours.Add("12PM");
            hours.Add("1PM");
            hours.Add("2PM");
            hours.Add("3PM");
            hours.Add("4PM");
            InitializeComponent();

            SelectedMonth.SelectedValue = getMonthByPage();

            int row = 0;
            foreach(string s in hours)
            {
                // rowContainer is the wrapper for every calendar row
                // which corresponds to 1 hour in the calendar
                Border rowContainer = new Border();

                // tb is the hour (e.g. 3PM) textblock
                TextBlock tb = new TextBlock();
                tb.Text = s;
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
                hcd.Width = (GridLength) glc.ConvertFromString("0.5*");
                
                Border hourCellContainer = new Border();
                hourCellContainer.Child = tb;
                hourCellContainer.BorderThickness = new Thickness(0, 0, 1, 0);
                hourCellContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                hourCellContainer.Padding = new Thickness(2);

                columns.ColumnDefinitions.Add(hcd);
                columns.Children.Add(hourCellContainer);

                for (int i = 0; i < 7; i++)
                {
                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = (GridLength)glc.ConvertFromString("1*");
                    columns.ColumnDefinitions.Add(cd);

                    Grid appointmentCell = new Grid();
                    Border appointmentCellContainer = new Border();
                    appointmentCellContainer.BorderThickness = new Thickness(0, 0, 1, 0);
                    appointmentCellContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                    appointmentCellContainer.SetValue(Grid.ColumnProperty, i+1);
                    for (int j = 0; j < 2; j++)
                    {
                        if ((i * row + (j)) % 3 == 0 || (i * row + (j)) == 9)
                        {
                            Border appointment = new Border();

                            string[] names = {"Deeann Tezure","Dulcy Pretorius","Sheffield Peregrine","Seka Wade","Vachel Raselles","Stoddard Ferrarone","Cherise Cropton","Jeanne Percy","Tamarah Esherwood","Norry Nettle","Loy Latliff","Gipsy Le Estut","Leroi Ipwell","Randie Ludlow","Aline Smeath","Arnaldo Bolam","Yolane Duker","Cointon Stonhewer","Bartholomeus Spincks","Jonah Ygou","Reagan Obee","Lonnie Meedendorpe","Gene Kench","Conroy Umney","Mommy Hevner","Jamaal MacDermid","Stefano Sommerly","Willetta Pedrazzi","Frannie Somerset","Gloriana Hopkynson","Gertruda Dolligon","Aloise Galfour","Holli Duligall","Latrina Crawford","Durward Corlett","Bartholomeo Pynn","Jeanine Blaylock","Tiffanie O'Scollain","Edward Caroline","Murdoch Cicculini","Shaylyn Urlich","Hinze Glasscott","Mylo Adamowicz","Fields Pettifor","Lisbeth Phippard","Peria Loftus","Padriac Mereweather","Sunshine Rikel","Indira Lazell","Kakalina Mellon","Bernette Davie","Worthington Dashper","Violette Surmeyer","Shannah Glamart","Mag Leasor","Liam Harbidge","Irma Thickin","Micah Teligin","Amelina Yakubovics","Dennie Livick"};

                            appointment.Background = (new BrushConverter()).ConvertFromString("#dbeafe") as Brush;
                            appointment.Padding = new Thickness(4);
                            appointment.SetValue(Grid.RowProperty, j);
                            appointment.Margin = new Thickness(2, 1, 2, 1);

                            TextBlock appointmentInfo = new TextBlock();
                            appointmentInfo.Text = "Appointment with " + names[(i * row + (j)) % 20];
                            appointmentInfo.TextWrapping = TextWrapping.Wrap;
                            appointmentInfo.VerticalAlignment = VerticalAlignment.Center;
                            appointmentInfo.Foreground = (new BrushConverter()).ConvertFromString("#1e3a8a") as Brush;
                            appointmentInfo.TextTrimming = TextTrimming.CharacterEllipsis;

                            appointment.Child = appointmentInfo;

                            RowDefinition appointmentCellRD = new RowDefinition();
                            appointmentCellRD.Height = (GridLength)glc.ConvertFromString("1*");
                            appointmentCell.RowDefinitions.Add(appointmentCellRD);

                            appointmentCell.Children.Add(appointment);
                        } else
                        {
                            Button appointmentButton = new Button();

                            appointmentButton.Background = Brushes.Transparent;
                            appointmentButton.BorderThickness = new Thickness(0);
                            appointmentButton.SetValue(Grid.RowProperty, j);

                            RowDefinition appointmentCellRD = new RowDefinition();
                            appointmentCellRD.Height = (GridLength)glc.ConvertFromString("1*");
                            appointmentCell.RowDefinitions.Add(appointmentCellRD);

                            appointmentCell.Children.Add(appointmentButton);
                        }
                    }
                    appointmentCellContainer.Child = appointmentCell;
                    columns.Children.Add(appointmentCellContainer);
                }

                rowContainer.Child = columns;

                // set RD for every calendar row
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(100);
                CalendarRows.RowDefinitions.Add(rd);
                
                CalendarRows.Children.Add(rowContainer);
            }

            renderCalendarDayRow();


            
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
                int index = (this.page - 1) * Days.Count + i;

                WrapPanel dayWP = new WrapPanel();
                dayWP.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock dayTB = new TextBlock();
                dayTB.Text = this.Days[i] + " ";

                TextBlock dateTB = new TextBlock();
                dateTB.Text = this.calendarDays[index].ToString();
                dateTB.FontWeight = FontWeights.Bold;

                dayWP.Children.Add(dayTB);
                dayWP.Children.Add(dateTB);

                Border dateContainer = new Border();
                dateContainer.BorderThickness = new Thickness(0, 0, 0, 1);
                dateContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                dateContainer.Padding = new Thickness(2);
                dateContainer.Child = dayWP;
                dateContainer.SetValue(Grid.ColumnProperty, i+1);

                CalendarDaysRow.Children.Add(dateContainer);
            }

        }

        private void NextWeek(object sender, RoutedEventArgs e)
        {
            this.page++;

            if (this.page > this.getMaxPage())
            {
                this.page = this.getMaxPage();
            }

            renderCalendarDayRow();
            SelectedMonth.SelectedValue = getMonthByPage();
        }

        private void PreviousWeek(object sender, RoutedEventArgs e)
        {
            this.page--;

            if (this.page < 1)
            {
                this.page = 1;
            }

            renderCalendarDayRow();
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

            renderCalendarDayRow();
        }
    }
}
