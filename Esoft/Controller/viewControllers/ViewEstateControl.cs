using Esoft.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Collections;

namespace Esoft.Controller.viewControllers
{
    internal class ViewEstateControl
    {
        protected EstateControl estateControl = new EstateControl();

        public async Task FillingDataGridEstate(DataGrid dataGrid)
        {
            try
            {
                IEnumerable items = await estateControl.GetEstates();

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
