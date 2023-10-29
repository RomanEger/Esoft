using Esoft.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfApp1.Controller;

namespace Esoft.Controller
{
    internal class ViewClientControl
    {
        protected ClientControl clientControl = new ClientControl();

        public async Task FillingDataGridClient(DataGrid dataGrid)
        {
            try
            {
                List<Client> items = await clientControl.GetClients();

                dataGrid.ItemsSource = items;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалось заполнить список");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось заполнить список");
                Console.WriteLine(ex.Message);
            }

        }
    }
}
