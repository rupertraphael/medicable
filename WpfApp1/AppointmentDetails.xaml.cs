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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AppointmentDetails : Page
    {

        public AppointmentDetails()
        {
            InitializeComponent();
        }

        public AppointmentDetails(string patientName, string doctorName)
        {        
            
            Dictionary<string, int> doctors = new Dictionary<string, int>();
            doctors.Add("Dr. Raphael", 0);
            doctors.Add("Dr. Amr", 1);
            doctors.Add("Dr. Rupert", 2);
            doctors.Add("Dr. Araiz", 3);
            doctors.Add("Dr. Chirag", 4);
            InitializeComponent();
            PatientName.Text = patientName;
            Doctors.SelectedIndex = doctors[doctorName];
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
