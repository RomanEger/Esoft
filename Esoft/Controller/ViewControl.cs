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
        readonly private ModelControl modelControl = new ModelControl();

        private static List<Page> backListPages = new List<Page>();
        public List<Page> FwdListPages;



        private static int backIndex = -1;

        public int BackIndex
        {
            get
            {
                if (backIndex > 0)
                    return --backIndex;
                else
                    return backIndex;
            }
            
        }




        public List<Page> BackListPages
        {
            get { return backListPages; } 
        }

        public void AddPageToBackListPages(Page page)
        {
            bool isUniquePage = true;
            foreach (var pages in backListPages)
            {
                if(pages.Title == page.Title)
                    isUniquePage = false;
            }
            if (isUniquePage) 
            {
                backListPages.Add(page);
                backIndex++;
            }
        }


        
        public async Task FillingDataGridClient(DataGrid dataGrid)
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

        public async Task FillingDataGridRealtor(DataGrid dataGrid)
        {
            try
            {
                List<Realtor> items = await modelControl.GetRealtors();

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
