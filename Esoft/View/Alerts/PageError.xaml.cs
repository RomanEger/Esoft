﻿using System;
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

namespace WpfApp1.View.Alerts
{
    /// <summary>
    /// Логика взаимодействия для PageError.xaml
    /// </summary>
    public partial class PageError : Page
    {
        public PageError(string error)
        {
            InitializeComponent();

            labelAlert.Content = error;

            //var uri = new Uri(".\\Resources\\ic_error_red_36pt.png", UriKind.Relative);

            //imgError.Source = new BitmapImage(uri);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
