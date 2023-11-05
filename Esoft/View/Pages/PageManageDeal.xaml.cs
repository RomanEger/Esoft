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
