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
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page

    {
        public List<APatient> patientList = new List<APatient>();
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

            patientList.Add(new APatient("Scott Turner", "403-555-1430", "13256-1231", "03/02/1983", "73 5 Ave NW Calgary AB", "Dr Rupert", "", ""));
            patientList.Add(new APatient("Rosy Usher", "403-555-6122", "11661-1209", "07/15/2002", "65 Hills Rd NE Calgary AB", "Dr Amr", "", ""));
            patientList.Add(new APatient("Linda Walsh", "403-555-1112", "15671-1200", "01/01/2000", "86 1 ST SE Calgary AB", "Dr Chirag", "", ""));
            patientList.Add(new APatient("Albert Zander", "403-555-1430", "13112-1764", "09/22/1967", "123 Martin Crescent NE Calgary AB", "Dr Raphael", "", ""));
            patientList.Add(new APatient("Antony Simmons", "587-412-8666", "16543-1289", "11/19/2005", "432 Panaroma RD NW Calgary AB", "Dr Araiz", "", ""));
            patientList.Add(new APatient("Bruno Simmons", "587-222-8656", "16552-1139", "05/06/1945", "170 Ridge ST SW Calgary AB", "", "", ""));
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

        private void PatientView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            APatient client = (APatient) this.patientView.SelectedItem;
            if (client != null)
            {
                clientName.Content = client.PatientName;
                clientHealthID.Text = client.PatientHealthCareNumber;
                clientPhoneNumber.Text = client.PatientPhoneNumber;
                clientDOB.Text = client.PatientDOB;
                clientAddress.Text = client.PatientAddress;
                clientPreferredDoctor.Text = client.PatientPreferredDoctor;
            }
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
    }
}
