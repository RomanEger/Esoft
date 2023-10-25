using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WpfApp1.View;
using NUnit.Framework;
using Esoft.Controller;
using System.Data.SqlClient;
using Esoft.Model;

namespace WpfApp1.Controller
{
    internal class ViewControl
    {
        public static Frame frame;

        readonly private ModelControl  modelControl = new ModelControl();
        //str = str.Replace(" ", string.Empty);

        //        for (int j = 0; j<str.Length; j++)
        //            if (!char.IsDigit(str[j]))
        //                str = str.Remove(j, 1);

        public async void FillingDataGridClient(DataGrid dataGrid)
        {
            try
            {
                List<Client> items = await modelControl.GetClients();

                dataGrid.ItemsSource = items;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалось заполнить список");
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось заполнить список");
                Console.WriteLine(ex.Message);
            }

        }

    }
}
