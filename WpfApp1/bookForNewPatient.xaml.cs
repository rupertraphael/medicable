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

        }

        private void CreatePatientBtn_Click(object sender, RoutedEventArgs e)
        {
           // if(First_name_box.Text == null)
        
            //patientList.Add(new APatient(First_name_box.Text + " " + Last_name_box.Text, phone_box.Text, health_id_box.Text, dob_box.Text, address_box.Text, "", conditions_box.Text, allergy_box.Text));

            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new AppointmentDetails());
  
        }
    }
}
