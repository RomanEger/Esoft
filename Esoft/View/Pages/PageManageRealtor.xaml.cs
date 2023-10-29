using Esoft.Controller;
using Esoft.Controller.viewControllers;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Controller;

namespace Esoft.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageManageRealtor.xaml
    /// </summary>
    public partial class PageManageRealtor : Page
    {
        RealtorControl modelControl;
        ViewRealtorControl viewRealtorControl;
        ViewControl viewControl;
        public PageManageRealtor()
        {
            InitializeComponent();

            modelControl = new RealtorControl();
            viewRealtorControl = new ViewRealtorControl();
            viewControl = new ViewControl();

            colDemands.Header = "Кол-во\nпотребностей";
            colOffers.Header = "Кол-во\nпредложений";

            var task = viewRealtorControl.FillingDataGridRealtor(dgRealtors);
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

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            await viewRealtorControl.Search(tbSearch.Text, dgRealtors);
        }
    }
}
