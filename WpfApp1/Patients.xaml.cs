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

            patientList.Add(new APatient("Scott Turner", "403-555-1430", "13256-1231", "", "", ""));
            patientList.Add(new APatient("Rosy Usher", "403-555-6122", "11661-1209", "", "", ""));
            patientList.Add(new APatient("Linda Walsh", "403-555-1112", "15671-1200", "", "", ""));
            patientList.Add(new APatient("Albert Zander", "403-555-1430", "13112-1764", "", "", ""));
            patientList.Add(new APatient("Antony Simmons", "587-412-8666", "16543-1289", "", "", ""));
            SearchBar.Text = "Search by Name, Phone Number, Alberta HealthCare # ";
            patientView.ItemsSource = patientList;
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
            patientView.ItemsSource = outputPatientList;
        }
        private void SearchBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchBar.Text = "";
        }
    }
}
