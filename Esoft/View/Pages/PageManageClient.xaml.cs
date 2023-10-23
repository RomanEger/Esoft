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

        public PageManageClient(int tab)
        {
            InitializeComponent();


            modelControl = new ModelControl();

            Clients.SelectedItem = tab == 1 ? tabAddClient : 
                                   tab == 2 ? tabUpdateClient : 
                                              tabDelClient;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            if(!ViewControl.IsValidEmail(tbEmail.Text) && 
                (tbMobileNumber.Text == string.Empty || !ViewControl.IsValidMobileNumber(tbMobileNumber.Text)))
            {
                popupError.IsOpen = true;
                labelPopup.Content = "Некорректный email";
            }
            else if(!ViewControl.IsValidMobileNumber(tbMobileNumber.Text) &&
                (tbEmail.Text == string.Empty || !ViewControl.IsValidEmail(tbEmail.Text)))
            {
                popupError.IsOpen = true;
                labelPopup.Content = "Некорректный номер телефона\nВведите номер телефона в формате 8хххххххххх";
            }
            else
            {
                modelControl.AddClient(string.Concat(
                                        new string[] {
                                            tbLastName.Text, " ", 
                                            tbName.Text, " ", 
                                            tbPatronymic.Text, " ", 
                                            tbEmail.Text, " ", 
                                            tbMobileNumber.Text}));
            }
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            popupError.IsOpen = false;
        }
    }
}
