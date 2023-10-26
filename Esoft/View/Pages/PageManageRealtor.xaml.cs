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
    /// Логика взаимодействия для PageManageRealtor.xaml
    /// </summary>
    public partial class PageManageRealtor : Page
    {
        ModelControl modelControl;
        ViewControl viewControl;

        public PageManageRealtor()
        {
            InitializeComponent();

            modelControl = new ModelControl();
            viewControl = new ViewControl();


            var task = viewControl.FillingDataGridRealtor(dgRealtors);
        }

        private void btnUpdateDg_Click(object sender, RoutedEventArgs e)
        {
            PageManageRealtor page = new PageManageRealtor();

            viewControl.AddPageToBackListPages(page);
            ViewControl.frame.Navigate(page);
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Изменение", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                await modelControl.AddOrUpdateRealtor(dgRealtors.ItemsSource);

                ViewControl.frame.Navigate(new PageManageRealtor());
            }
        }

        private async void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Удаление", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                await modelControl.RemoveRealtor(dgRealtors.SelectedItems);


                ViewControl.frame.Navigate(new PageManageRealtor());
            }
        }
    }
}
