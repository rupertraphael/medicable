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
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private Dictionary<string, Page> pages = new Dictionary<string, Page>();
        private Dashboard DashboardPage = new Dashboard();
        private Patients PatientsPage = new Patients();


        public MainWindow()
        {
            InitializeComponent();

            //this.DashboardPage.Loaded += DashboardPage_Loaded;
            //this.PatientsPage.Loaded += PatientsPage_Loaded;

            DashboardPage.PatientsPageLink = PatientsPage;

            MainNavFrame.LoadCompleted += MainNavFrame_LoadCompleted;
        }

        private void MainNavFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Content.GetType() == PatientsPage.GetType() || e.Content.GetType() == (new Calendar()).GetType())
            {
                Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
                Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;

                
            } else if(e.Content.GetType() == DashboardPage.GetType())
            {
                Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
                Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
            }
        }

        private void DashboardPage_Loaded(object sender, RoutedEventArgs e)
        {
            Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
            Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
        }

        private void PatientsPage_Loaded(object sender, RoutedEventArgs e)
        {
            Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
            Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
        }


        private void Dashboard_Button_Click(object sender, RoutedEventArgs e)
        {
            //Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
            //Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
            //Patients_Button.FontWeight = FontWeights.Regular;

            //MainNavFrame.Source = new Uri("Dashboard.xaml", UriKind.Relative);

            MainNavFrame.NavigationService.Navigate(this.DashboardPage);
        }

        private void Patients_Button_Click(object sender, RoutedEventArgs e)
        {
            //Patients_Button.Background = (new BrushConverter()).ConvertFromString("#1e40af") as Brush;
            //Dashboard_Button.Background = (new BrushConverter()).ConvertFromString("#1d4ed8") as Brush;
            //Dashboard_Button.FontWeight = FontWeights.Regular;

            MainNavFrame.NavigationService.Navigate(this.PatientsPage);
        }
    }
}
