using Esoft.Controller;
using Esoft.Controller.viewControllers;
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
    /// Логика взаимодействия для PageManageEstate.xaml
    /// </summary>
    public partial class PageManageEstate : Page
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Title = "Меню";
        }

        EstateControl estateControl;
        ViewEstateControl viewEstateControl;
        ViewControl viewControl

        public PageManageEstate()
        {
            InitializeComponent();

            estateControl = new EstateControl();
            viewEstateControl = new ViewEstateControl();
            viewControl = new ViewControl();

            colHouseNumb.Header = "Номер\nдома";
            colNumbRooms.Header = "Кол-во\nкомнат";
            colOffers.Header = "Кол-во\nпредложений";
            _ = viewEstateControl.FillingDataGridEstate(dgEstates);
        }

        private async void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Удаление", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                await estateControl.RemoveEstate(dgEstates.SelectedItems);


                ViewControl.frame.Navigate(new PageManageClient());
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnUpdateDg_Click(object sender, RoutedEventArgs e)
        {
            PageManageEstate page = new PageManageEstate();

            viewControl.AddPageToBackListPages(page);
            ViewControl.frame.Navigate(page);
        }
    }
}
