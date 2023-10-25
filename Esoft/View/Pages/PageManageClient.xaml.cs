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
        ModelControl modelControl;
        MyValidator validator;
        ViewControl viewControl;
        public PageManageClient(int tab)
        {
            InitializeComponent();

            modelControl = new ModelControl();
            validator = new MyValidator();
            viewControl= new ViewControl();

            Clients.SelectedItem = tab == 1 ? tabAddClient : tabUpdateClient;

            viewControl.FillingDataGridClient(dgClients);

        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            if(!validator.IsValidEmail(tbEmail.Text) && 
                (tbMobileNumber.Text == string.Empty || !validator.IsValidMobileNumber(tbMobileNumber.Text)))
            {
                popupError.IsOpen = true;
                labelPopup.Content = "Некорректный email";
            }
            else if(!validator.IsValidMobileNumber(tbMobileNumber.Text) &&
                (tbEmail.Text == string.Empty || !validator.IsValidEmail(tbEmail.Text)))
            {
                popupError.IsOpen = true;
                labelPopup.Content = "Некорректный номер телефона\nВведите номер телефона в формате 8хххххххххх";
            }
            else
            {
                await modelControl.AddClient(string.Concat(
                                        new string[] {
                                            tbLastName.Text, " ", 
                                            tbName.Text, " ", 
                                            tbPatronymic.Text, " ", 
                                            tbEmail.Text, " ", 
                                            tbMobileNumber.Text}));
            }
            viewControl.FillingDataGridClient(dgClients);
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            popupError.IsOpen = false;
        }

        private  void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Изменение", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {

                var task = modelControl.UpdateClient(dgClients.ItemsSource);

            }
        }

        

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Удаление", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                //List<Client> remove = (List<Client>)dgClients.ItemsSource;

                //removeClients.AddRange(remove);

                var task = modelControl.RemoveClient(dgClients.SelectedItems);

            }
        }

        private void btnUpdateDg_Click(object sender, RoutedEventArgs e)
        {
            ViewControl.frame.Navigate(new PageManageClient(2));
        }
    }
}
