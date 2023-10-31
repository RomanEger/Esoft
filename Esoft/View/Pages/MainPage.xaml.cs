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
using WpfApp1.Controller;

namespace Esoft.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        PageManageClient pageManageClient;
        PageManageRealtor pageManageRealtor;
        PageManageEstate pageManageEstate;

        ViewControl viewControl;
        public MainPage()
        {
            InitializeComponent();

            viewControl = new ViewControl();
        }

        private void btnManageClient_Click(object sender, RoutedEventArgs e)
        {
            pageManageClient = new PageManageClient();
            
            viewControl.AddPageToBackListPages(pageManageClient);
            ViewControl.frame.Navigate(pageManageClient);
        }

        private void btnManageRealtor_Click(object sender, RoutedEventArgs e)
        {
            pageManageRealtor = new PageManageRealtor();

            viewControl.AddPageToBackListPages(pageManageRealtor);
            ViewControl.frame.Navigate(pageManageRealtor);
        }

        private void btnManageEstate_Click(object sender, RoutedEventArgs e)
        {
            pageManageEstate = new PageManageEstate();

            viewControl.AddPageToBackListPages(pageManageEstate);
            ViewControl.frame.Navigate(pageManageEstate);
        }

        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Title = "Меню";
        }
    }
}
