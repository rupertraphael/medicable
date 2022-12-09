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
            emptyPatientView.HorizontalAlignment= HorizontalAlignment.Center;
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
                errorHealthID.Text = "";
            else
                errorHealthID.Text = "Incorrect Format (#####-####)";

        }
    }
}
