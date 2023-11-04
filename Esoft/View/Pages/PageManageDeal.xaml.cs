using Esoft.Controller.modelContollers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Controller;

namespace Esoft.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageManageDeal.xaml
    /// </summary>
    public partial class PageManageDeal : Page
    {
        DealControl dealControl;

        int offer = -1;
        int demand = -1;

        int idClient = 0;
        int clientsCount;
        public PageManageDeal()
        {
            InitializeComponent();

            dealControl = new DealControl();

            FillingPageAsync(idClient);
        }

        private async void FillingPageAsync(int i)
        {
            var list = await dealControl.GetClientsAsync();
            clientsCount = list.Count;
            if (i < clientsCount)
            {
                labClientInfo.Content = list[i].ToString();
                int id = int.Parse(list[i].ToString().Split(' ')[0]);
                await FillingDgDemandsAsync(id);
                await FillingDgOffersAsync(id);
            }
            else
            {
                idClient = 0;
                FillingPageAsync(idClient);
            }
        }

        private async Task FillingDgDemandsAsync(int i)
        {
            var listDemands = await dealControl.GetDemandsAsync(i);

            dgDemands.ItemsSource = listDemands;
        }

        private async Task FillingDgOffersAsync(int id)
        {
            var listOffers = await dealControl.GetOffersAsync(id);

            dgOffers.ItemsSource = listOffers;
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

        private void btnSelectDemand_Click(object sender, RoutedEventArgs e)
        {
            var row = dgDemands.SelectedItem;



            string str = row.ToString();
            var sb = new StringBuilder();
            for (int i = 13; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    sb.Append(str[i]);
                else
                    break;
            }
            str = sb.ToString();

            demand = int.Parse(str);
        }

        private void btnSelectOffer_Click(object sender, RoutedEventArgs e)
        {
            var row = dgOffers.SelectedItem;

            string str = row.ToString();
            var sb = new StringBuilder();
            for (int i = 12; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    sb.Append(str[i]);
                else
                    break;
            }
            str = sb.ToString();
            if(!string.IsNullOrEmpty(str))
            offer = int.Parse(str);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            FillingPageAsync(++idClient);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if(idClient > 0)
                FillingPageAsync(--idClient);
            else
            {
                idClient  = clientsCount - 1;
                FillingPageAsync(idClient);
            }
        }
    }
}
