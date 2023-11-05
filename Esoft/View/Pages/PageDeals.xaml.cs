using Esoft.Controller.modelContollers;
using Esoft.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Esoft.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageDeals.xaml
    /// </summary>
    public partial class PageDeals : Page
    {
        ManageDealControl dealControl;

        int demand = -1;
        int offer = -1;

        public PageDeals()
        {
            InitializeComponent();
            dealControl = new ManageDealControl();

            FillingCmbDemands();
        }

        private async void FillingCmbDemands()
        {
            var list = await dealControl.GetDemandsAsync();
            dgDemands.ItemsSource = list;
        }


        private async Task FillingDgOffersAsync()
        {
            try
            {

                var list = await dealControl.GetOffersAsync(demand);

                dgOffers.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnAddDeal_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Сохранение", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (demand < 0 || offer < 0)
                    return;
                await dealControl.AddDealAsync(demand, offer);

            }
        }

        private async void btnChooseDemand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Demand row = (Demand)dgDemands.SelectedItem;

                demand = row.Id;


                await FillingDgOffersAsync();
            }
            catch
            {
                MessageBox.Show("Не удалось выбрать потребность");
            }
        }

        private void btnChooseOffer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Offer row = (Offer)dgOffers.SelectedItem;

                offer = row.Id;
            }
            catch
            {
                MessageBox.Show("Не удалось выбрать предложение");
            }
        }
    }
}
