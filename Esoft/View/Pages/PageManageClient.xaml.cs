using Esoft.Controller;
using Esoft.Model;
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

namespace Esoft.View.Pages
{
    //Реализуйте интерфейс, который позволит пользователю осуществлять операции создания нового клиента, 
    //    обновления информации о клиенте, удаления клиента.

    //Интерфейс должен не позволять пользователю создать клиента без указания номера телефона или электронной почты,
    //    удалять клиента, связанного с потребностью или предложением.



    /// <summary>
    /// Логика взаимодействия для PageManageClient.xaml
    /// </summary>
    public partial class PageManageClient : Page
    {
        ClientControl modelControl;
        ViewClientControl viewClientControl;
        ViewControl viewControl;
        public PageManageClient()
        {
            InitializeComponent();

            modelControl = new ClientControl();
            viewClientControl= new ViewClientControl();
            viewControl = new ViewControl();

            var task = viewClientControl.FillingDataGridClient(dgClients);

            colDemands.Header = "Кол-во\nпотребностей";
            colOffers.Header = "Кол-во\nпредложений";
        }

        

        //private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        //{
        //    popupError.IsOpen = false;
        //}

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Изменение", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                await modelControl.AddOrUpdateClient(dgClients.ItemsSource);

                ViewControl.frame.Navigate(new PageManageClient());
            }
        }

        

        private async void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Удаление", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                await modelControl.RemoveClient(dgClients.SelectedItems);


                ViewControl.frame.Navigate(new PageManageClient());
            }
        }

        private void btnUpdateDg_Click(object sender, RoutedEventArgs e)
        {
            PageManageClient page = new PageManageClient();

            viewControl.AddPageToBackListPages(page);
            ViewControl.frame.Navigate(page);
        }
    }
}
