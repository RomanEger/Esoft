using Esoft.Model;
using Esoft.View.Pages;
using System;
using System.Collections;
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
using WpfApp1.Controller;
using WpfApp1.View.Pages;

namespace Esoft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewControl viewControl;
        MainPage mainPage;
        public MainWindow()
        {
            InitializeComponent();

            ResizeMode = ResizeMode.CanMinimize;

            ViewControl.frame = frameMain;
            viewControl = new ViewControl();

            mainPage = new MainPage();

            viewControl.AddPageToBackListPages(mainPage);
            frameMain.Navigate(mainPage);

            //ModelControl.esoftDB = new esoftDBEntities();
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(viewControl.BackListPages.Count > 1)
            {
                int i = viewControl.BackIndex;
                frameMain.Navigate(viewControl.BackListPages[i]);
            }

        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(mainPage);
        }

        private void frameMain_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
