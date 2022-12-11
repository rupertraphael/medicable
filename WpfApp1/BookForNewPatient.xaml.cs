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
using System.Xml.Linq;
using System.ComponentModel;
using static WpfApp1.MainWindow;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for bookForNewPatient.xaml
    /// </summary>
    public partial class BookForNewPatient : Page
    {


        public BookForNewPatient()
        {
            InitializeComponent();

            First_name1.Focus();

        }

        List<APatient> patientList = DB.APatients;

        private void CreatePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Appointment> blank = new List<Appointment>();
            if (!errorFirstName.Text.Equals("") || !errorLastName.Text.Equals("") || !errorHealthID1.Text.Equals("") || !errorDOB1.Text.Equals("") || !errorAddress1.Text.Equals("") || !errorPhone.Text.Equals(""))
            {
                string messageBoxText = "Please Make There Are No Errors Before Proceeding";
                string caption = "Errors!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            }
            else
            {
                patientList.Add(new APatient(First_name_box.Text + " " + Last_name_box.Text, phone_box.Text, health_id_box.Text, dob_box.Text, address_box.Text, "", "", "", blank));
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new Patients());
            }
        }

        private void phone_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(phone_box.Text, "[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (correctFormat && phone_box.Text.Length == 12)
                errorPhone.Text = "";
            else if (phone_box.Text.Length == 0)
            {
                errorPhone.Text = "Enter Phone Number (111-111-1111)";
            }
            else
                errorPhone.Text = "Incorrect Format (111-111-1111)";
        }

        private void health_id_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(health_id_box.Text, "[0-9]{5}-[0-9]{4}");
            if (correctFormat && health_id_box.Text.Length == 10)
                errorHealthID1.Text = "";
            else if (health_id_box.Text.Length == 0)
            {
                errorHealthID1.Text = "Enter Health ID (11111-1111)";
            }
            else
                errorHealthID1.Text = "Incorrect Format (11111-1111)";
        }

        private void DOB_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(dob_box.Text, "[0-9]{2}/[0-9]{2}/[0-9]{4}");

            if (dob_box.Text.Length == 10)
            {
                string month = dob_box.Text.Substring(0, 2);
                string day = dob_box.Text.Substring(3, 2);
                string year = dob_box.Text.Substring(6, 4);

                if (Convert.ToInt32(month) < 1 || Convert.ToInt32(month) > 12)
                {
                    errorDOB1.Text = "Incorrect Month Entry (1-12) (MM/DD/YYYY)";
                }

                else if (Convert.ToInt32(day) < 1 || Convert.ToInt32(day) > 31)
                {
                    errorDOB1.Text = "Incorrect Day Entry (1-31) (MM/DD/YYYY)";
                }

                else if (Convert.ToInt32(year) > 2022)
                {
                    errorDOB1.Text = "Incorrect Year Entry (No More Than 2022) (MM/DD/YYYY)";
                }
                else if (correctFormat && dob_box.Text.Length == 10)
                {
                    errorDOB1.Text = "";
                }
                else
                {
                    errorDOB1.Text = "Incorrect Format (MM/DD/YYYY)";
                }
            }
            else if (dob_box.Text.Length == 0)
            {
                errorDOB1.Text = "Enter Date Of Birth (MM/DD/YYYY)";
            }
            else
                errorDOB1.Text = "Incorrect Format (MM/DD/YYYY)";
        }

        private void First_name_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (First_name_box.Text.Length == 0)
            {
                errorFirstName.Text = "Enter First Name";
            }
            else
            {
                errorFirstName.Text = "";
            }
        }

        private void Last_name_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Last_name_box.Text.Length == 0)
            {
                errorLastName.Text = "Enter Last Name";
            }
            else
            {
                errorLastName.Text = "";
            }
        }

        private void address_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (address_box.Text.Length == 0)
            {
                errorAddress1.Text = "Enter Address";
            }
            else
            {
                errorAddress1.Text = "";
            }
        }

        private void First_name_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (First_name_box.Text.Length != 0)
            {
                errorFirstName.Text = "";
            }
        }

        private void Last_name_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Last_name_box.Text.Length != 0)
            {
                errorLastName.Text = "";
            }
        }

        private void address_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (address_box.Text.Length != 0)
            {
                errorAddress1.Text = "";
            }
        }

        private void phone_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(phone_box.Text, "[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (correctFormat && phone_box.Text.Length == 12)
                errorPhone.Text = "";
        }

        private void dob_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(dob_box.Text, "[0-9]{2}/[0-9]{2}/[0-9]{4}");
            if (correctFormat && dob_box.Text.Length == 10)
            {
                errorDOB1.Text = "";
            }
        }

        private void health_id_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(health_id_box.Text, "[0-9]{5}-[0-9]{4}");
            if (correctFormat && health_id_box.Text.Length == 10)
                errorHealthID1.Text = "";
        }
    }
}