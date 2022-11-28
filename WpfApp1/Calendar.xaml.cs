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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        public Calendar()
        {

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

                for (int i = 1; i < 7; i++)
                {
                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = (GridLength)glc.ConvertFromString("1*");
                    columns.ColumnDefinitions.Add(cd);

                    Grid appointmentCell = new Grid();
                    Border appointmentCellContainer = new Border();
                    appointmentCellContainer.BorderThickness = new Thickness(0, 0, 1, 0);
                    appointmentCellContainer.BorderBrush = (new BrushConverter()).ConvertFromString("#d1d5db") as Brush;
                    appointmentCellContainer.SetValue(Grid.ColumnProperty, i);
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


            
        }
    }
}
