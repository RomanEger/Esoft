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
using System.Data;

namespace Esoft.Controller.viewControllers
{
    internal class ViewEstateControl
    {
        protected EstateControl estateControl = new EstateControl();

        public async Task GetTaskAsync(DataGrid dataGrid, ComboBox cmb1, ComboBox cmb2)
        {
            await FillingDataGridEstate(dataGrid);
            await FillingCmbType(cmb1);
            await FillingCmbStreetAddress(cmb2);
        }


        private async Task FillingDataGridEstate(DataGrid dataGrid)
        {
            try
            {
                var items = await estateControl.GetEstates();

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

        private async Task FillingCmbType(ComboBox comboBox)
        {
            try
            {
                var list = await estateControl.FillingCmbType();
                list.Insert(0, "По умолчанию");
                comboBox.ItemsSource = list;
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

        private async Task FillingCmbStreetAddress(ComboBox comboBox)
        {
            try
            {
                var list = await estateControl.FillingCmbStreetAddress();
                list.Insert(0, "По умолчанию");
                comboBox.ItemsSource = list;
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

        public async Task SearchAsync(DataGrid dataGrid, string srcText = null, string filterType = null, string filterStreet = null)
        {
            try
            {
                var items = await estateControl.SearchAsync(srcText, filterType, filterStreet);
                dataGrid.ItemsSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
