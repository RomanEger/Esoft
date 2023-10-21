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
    /// Логика взаимодействия для PageAddClient.xaml
    /// </summary>
    public partial class PageAddClient : Page
    {
        ModelControl modelControl;

        public PageAddClient()
        {
            InitializeComponent();

            modelControl = new ModelControl();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            if(!ViewControl.IsValidEmail(tbEmail.Text))
            {
                popup1.IsOpen = true;
                
            }
            else if(!ViewControl.IsValidMobileNumber(tbMobileNumber.Text))
            {
                popup1.IsOpen = true;
            }
            else
            {
                modelControl.AddClient(string.Concat(new string[] { tbLastName.Text, " ", tbName.Text, " ", tbPatronymic.Text, " ", tbEmail.Text, " ", tbMobileNumber.Text}));
            }
        }
    }
}
