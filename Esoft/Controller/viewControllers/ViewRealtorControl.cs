using System;
using System.Collections;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Esoft.Controller.viewControllers
{
    internal class ViewRealtorControl
    {
        protected RealtorControl realtorControl = new RealtorControl();

        public async Task FillingDataGridRealtor(DataGrid dataGrid)
        {
            try
            {
                IEnumerable items = await realtorControl.GetRealtors();

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

        public async Task Search(string search, DataGrid dataGrid)
        {
            try
            {
                IEnumerable items = await realtorControl.SearchRealtors(search);

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
