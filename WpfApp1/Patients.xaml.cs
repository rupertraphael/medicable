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
        private APatient selectedPatient;
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
                // select patient
                selectedPatient = client;

                clientName.Content = client.PatientName;
                clientHealthID.Text = client.PatientHealthCareNumber;
                clientPhoneNumber.Text = client.PatientPhoneNumber;
                clientDOB.Text = client.PatientDOB;
                clientAddress.Text = client.PatientAddress;
                clientPreferredDoctor.Text = client.PatientPreferredDoctor;

                PatientView.Child = PatientViewGrid;

                List<Appointment> appointments = DB.Appointments.Where(a => a.Patient.PatientHealthCareNumber == client.PatientHealthCareNumber).ToList();

                upcomingAppointments.ItemsSource = appointments;
            }
        }

        private void Book_Appointment(object sender, RoutedEventArgs e)
        {
            if (!errorHealthID.Text.Equals("") || !errorDOB.Text.Equals("") || !errorAddress.Text.Equals("") || !errorTelephone.Text.Equals(""))
            {
                string messageBoxText = "Please Make There Are No Errors Before Proceeding";
                string caption = "Errors!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                return;
            }

            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new AppointmentDetails(selectedPatient));
        }

        private void clientHealthID_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientHealthID.Text, "[0-9]{5}-[0-9]{4}");
            if (correctFormat && clientHealthID.Text.Length == 10)
            {
                patientView.IsHitTestVisible = true;
                errorHealthID.Text = "";
                APatient client = (APatient)this.patientView.SelectedItem;
                client.PatientHealthCareNumber = clientHealthID.Text;
            }
            else if(clientHealthID.Text.Length == 0)
            {
                patientView.IsHitTestVisible = false;
                errorHealthID.Text = "Enter Health ID (11111-1111)";
            }
            else
            {
                patientView.IsHitTestVisible = false;
                errorHealthID.Text = "Incorrect Format (11111-1111)";
            }
        }

        private void clientPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientPhoneNumber.Text, "[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (correctFormat && clientPhoneNumber.Text.Length == 12)
            {
                patientView.IsHitTestVisible = true;
                errorTelephone.Text = "";
                APatient client = (APatient)this.patientView.SelectedItem;
                client.PatientPhoneNumber = clientPhoneNumber.Text;
            }
            else if (clientPhoneNumber.Text.Length == 0)
            {
                patientView.IsHitTestVisible = false;
                errorTelephone.Text = "Enter Phone Number (111-111-1111)";
            }
            else
            {
                patientView.IsHitTestVisible = false;
                errorTelephone.Text = "Incorrect Format (111-111-1111)";
            }
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
                {
                    patientView.IsHitTestVisible = false;
                    errorDOB.Text = "Incorrect Month Entry (1-12) (MM/DD/YYYY)";
                }

                else if (Convert.ToInt32(day) < 1 || Convert.ToInt32(day) > 31)
                {
                    patientView.IsHitTestVisible = false;
                    errorDOB.Text = "Incorrect Day Entry (1-31) (MM/DD/YYYY)";
                }

                else if (Convert.ToInt32(year) > 2022)
                {
                    patientView.IsHitTestVisible = false;
                    errorDOB.Text = "Incorrect Year Entry (No More Than 2022) (MM/DD/YYYY)";
                }
                else if (correctFormat && clientDOB.Text.Length == 10)
                {
                    patientView.IsHitTestVisible = true;
                    errorDOB.Text = "";
                    APatient client = (APatient)this.patientView.SelectedItem;
                    client.PatientDOB = clientDOB.Text;
                }
                else
                {
                    patientView.IsHitTestVisible = false;
                    errorDOB.Text = "Incorrect Format (MM/DD/YYYY)";
                }
            }
            else if (clientDOB.Text.Length == 0)
            {
                patientView.IsHitTestVisible = false;
                errorDOB.Text = "Enter Date Of Birth (MM/DD/YYYY)";
            }
            else
                errorDOB.Text = "Incorrect Format (MM/DD/YYYY)";
        }

        private void clientHealthID_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientHealthID.Text, "[0-9]{5}-[0-9]{4}");
            if (correctFormat && clientHealthID.Text.Length == 10)
            {
                patientView.IsHitTestVisible = true;
                errorHealthID.Text = "";
            }
        }

        private void clientPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientPhoneNumber.Text, "[0-9]{3}-[0-9]{3}-[0-9]{4}");
            if (correctFormat && clientPhoneNumber.Text.Length == 12)
            {
                patientView.IsHitTestVisible = true;
                errorTelephone.Text = "";
            }
        }

        private void clientDOB_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool correctFormat = Regex.IsMatch(clientDOB.Text, "[0-9]{2}/[0-9]{2}/[0-9]{4}");
            if (correctFormat && clientDOB.Text.Length == 10)
            {
                patientView.IsHitTestVisible = true;
                errorDOB.Text = "";
            }
        }

        private void clientAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            if (clientAddress.Text.Length == 0)
            {
                patientView.IsHitTestVisible = false;
                errorAddress.Text = "Enter Address";
            }
            else
            {
                patientView.IsHitTestVisible = true;
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
                patientView.IsHitTestVisible = true;
                errorAddress.Text = "";
            }
        }
    }
    /*
     if (MainNavFrame.Content.GetType() == (new Calendar()).GetType() || MainNavFrame.Content.GetType() == (new AppointmentDetails()).GetType())
            {
                if(MessageBox.Show("You are currently booking an appointment. Do you want to cancel booking an appointment and go to another page?", "Cancel Appointment Booking", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }

            MainNavFrame.NavigationService.Navigate(this.DashboardPage);
    */
}
