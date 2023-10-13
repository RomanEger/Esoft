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
using WpfApp1.View.Alerts;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для AlertError.xaml
    /// </summary>
    public partial class AlertError : Window
    {
        public AlertError(string message, int idAlert)
        {
            InitializeComponent();

            ViewControl.frame = framePopup;

            switch (idAlert)
            {
                case 1: framePopup.Navigate(new PageError(message));
                    break;
                default:
                    break;
            }

        }

        
    }
}
