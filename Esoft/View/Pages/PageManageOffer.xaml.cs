using Esoft.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Controller;

namespace Esoft.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageManageOffer.xaml
    /// </summary>
    public partial class PageManageOffer : Page
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Title = "Управление предложениями";
        }

        OfferControl offerControl;
        ViewControl viewControl;

        public PageManageOffer()
        {
            InitializeComponent();

            offerControl = new OfferControl();
            viewControl = new ViewControl();

            FillingAsync();
        }

        private async void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Удаление", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {

                var row = dgOffers.SelectedItems;

                List<int> x = new List<int>();
                foreach (var r in row)
                {
                    string str = r.ToString();
                    var sb = new StringBuilder();
                    for (int i = 12; i < str.Length; i++)
                    {
                        if (char.IsDigit(str[i]))
                            sb.Append(str[i]);
                        else
                            break;
                    }
                    str = sb.ToString();

                    x.Add(int.Parse(str));

                }

                await offerControl.RemoveOffer(x);

                ViewControl.frame.Navigate(new PageManageOffer());
            }
        }

        private void btnUpdateDg_Click(object sender, RoutedEventArgs e)
        {
            PageManageOffer page = new PageManageOffer();

            viewControl.AddPageToBackListPages(page);
            ViewControl.frame.Navigate(page);
        }

        private async void FillingAsync()
        {
            await FillingDataGrid();
            await FillingComboBox();
        }

        private async Task FillingDataGrid()
        {
            try
            {
                var list = await offerControl.GetOffersAsync();

                dgOffers.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось заполнить список");
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int clientId = int.Parse(cmbClient.SelectedItem.ToString().Split(' ')[0]);

                int realtorId = int.Parse(cmbRealtor.SelectedItem.ToString().Split(' ')[0]);

                int estateId = int.Parse(cmbEstate.SelectedItem.ToString().Split(' ')[0]);

                int x;
                if (int.TryParse(tbPrice.Text, out x))
                    await offerControl.AddOffer(clientId, realtorId, estateId, x);
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить изменения");
            }
        }

        private async Task FillingComboBox()
        {
            var clients = await offerControl.FillingCmbClient();

            var realtors = await offerControl.FillingCmbRealtor();

            var estates = await offerControl.FillingCmbEstate();

            cmbClient.ItemsSource = clients;
            cmbRealtor.ItemsSource = realtors;
            cmbEstate.ItemsSource = estates;
        }

    }
}
