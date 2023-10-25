using Esoft.Model;
using Esoft.View.Pages;
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
using WpfApp1.View.Pages;

namespace Esoft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewControl.frame = frameMain;

            frameMain.Navigate(new PageManageClient(1));

            //ModelControl.esoftDB = new esoftDBEntities();

            
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
