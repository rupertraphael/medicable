using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page

    {
        private List<APatient> patientList = DB.APatients;
        public Patients()
        {
            InitializeComponent();

            // no patient selected
            TextBlock emptyPatientView = new TextBlock();
            emptyPatientView.Text = "Select a patient and you will see their details here.";
            emptyPatientView.HorizontalAlignment = HorizontalAlignment.Center;
            emptyPatientView.VerticalAlignment = VerticalAlignment.Center;
            emptyPatientView.FontSize = 24;
            PatientView.Child = emptyPatientView;

            patientView.ItemsSource = patientList.OrderBy(patient => patient.PatientName).ToList(); ;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = SearchBar.Text.ToUpper();
            List<APatient> outputPatientList = new List<APatient>();
            for (int i = 0; i < patientList.Count; i++)
            {
                if ((patientList[i].PatientName).ToUpper().Contains(input))
                {
                    outputPatientList.Add(patientList[i]);
                }
                else if ((patientList[i].PatientPhoneNumber).ToUpper().Contains(input))
                {
                    outputPatientList.Add(patientList[i]);
                }
                else if ((patientList[i].PatientHealthCareNumber).ToUpper().Contains(input))
                {
                    outputPatientList.Add(patientList[i]);
                }
            }
            patientView.ItemsSource = outputPatientList.OrderBy(patient => patient.PatientName).ToList();
        }
        private void SearchBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //SearchBar.Text = "";
        }



        private void patientView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            APatient client = (APatient)this.patientView.SelectedItem;
            if (client != null)
            {
                clientName.Content = client.PatientName;
                clientHealthID.Text = client.PatientHealthCareNumber;
                clientPhoneNumber.Text = client.PatientPhoneNumber;
                clientDOB.Text = client.PatientDOB;
                clientAddress.Text = client.PatientAddress;
                clientPreferredDoctor.Text = client.PatientPreferredDoctor;

                PatientView.Child = PatientViewGrid;
            }
        }

        private void Book_Appointment(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new AppointmentDetails());
        }

        private void clientHealthID_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientHealthID.Text, "[0-9]{5}-[0-9]{4}");
            if (correctFormat && clientHealthID.Text.Length == 10)
            {
                errorHealthID.Text = "";
                APatient client = (APatient)this.patientView.SelectedItem;
                client.PatientHealthCareNumber = clientHealthID.Text;
            }
            else
                errorHealthID.Text = "Incorrect Format (#####-####)";

        }

        private void clientPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientPhoneNumber.Text, "[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (correctFormat && clientPhoneNumber.Text.Length == 12)
            {
                errorTelephone.Text = "";
                APatient client = (APatient)this.patientView.SelectedItem;
                client.PatientPhoneNumber = clientPhoneNumber.Text;
            }
            else
                errorTelephone.Text = "Incorrect Format (###-###-####)";

        }

        private void clientDOB_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientDOB.Text, "[0-9]{2}/[0-9]{2}/[0-9]{4}");

            if (clientDOB.Text.Length == 10)
            {
                string month = clientDOB.Text.Substring(0, 2);
                string day = clientDOB.Text.Substring(3, 2);
                string year = clientDOB.Text.Substring(6, 4);

                if (Convert.ToInt32(month) < 1 || Convert.ToInt32(month) > 12)
                    errorDOB.Text = "Incorrect Month Entry (1-12) (MM/DD/YYYY)";

                else if (Convert.ToInt32(day) < 1 || Convert.ToInt32(day) > 31)
                    errorDOB.Text = "Incorrect Day Entry (1-31) (MM/DD/YYYY)";

                else if (Convert.ToInt32(year) > 2022)
                    errorDOB.Text = "Incorrect Year Entry (No More Than 2022) (MM/DD/YYYY)";

                else if (correctFormat && clientDOB.Text.Length == 10)
                {
                    errorDOB.Text = "";
                    APatient client = (APatient)this.patientView.SelectedItem;
                    client.PatientDOB = clientDOB.Text;
                }
                else
                    errorDOB.Text = "Incorrect Format (MM/DD/YYYY)";
            }
            else
                errorDOB.Text = "Incorrect Format (MM/DD/YYYY)";
        }

        private void clientHealthID_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientHealthID.Text, "[0-9]{5}-[0-9]{4}");
            if (correctFormat && clientHealthID.Text.Length == 10)
                errorHealthID.Text = "";
        }

        private void clientPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientPhoneNumber.Text, "[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (correctFormat && clientPhoneNumber.Text.Length == 12)
                errorTelephone.Text = "";
        }

        private void clientDOB_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientDOB.Text, "[0-9]{2}/[0-9]{2}/[0-9]{4}");
            if (correctFormat && clientDOB.Text.Length == 10)
                errorDOB.Text = "";
        }

        private void clientAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            if (clientAddress.Text.Length == 0)
            {
                errorAddress.Text = "Please Make Sure Address Field is not Blank";
            }
            else
            {
                errorAddress.Text = "";
                APatient client = (APatient)this.patientView.SelectedItem;
                client.PatientAddress = clientAddress.Text;
            }
        }

        private void clientPreferredDoctor_LostFocus(object sender, RoutedEventArgs e)
        {
            APatient client = (APatient)this.patientView.SelectedItem;
            client.PatientPreferredDoctor = clientPreferredDoctor.Text;
        }

        private void clientAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (clientAddress.Text.Length != 0)
            {
                errorAddress.Text = "";
            }
        }
    }
}
