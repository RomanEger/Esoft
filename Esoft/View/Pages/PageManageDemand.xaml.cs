using Esoft.Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Controller;

namespace Esoft.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageManageDemand.xaml
    /// </summary>
    public partial class PageManageDemand : Page
    {
        DemandControl demandControl;
        ViewControl viewControl;
        Parser parser;
        public PageManageDemand()
        {
            InitializeComponent();

            demandControl = new DemandControl();
            viewControl = new ViewControl();
            parser = new Parser();

            FillingAsync();
        }

        private async void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите принять внесенные изменения?", "Удаление", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var row = dgDemands.SelectedItems;

                List<int> x = new List<int>();
                foreach(var r in row)
                {
                    string str = r.ToString();
                    var sb = new StringBuilder();
                    for(int i = 13; i < str.Length; i++)
                    {
                        if (char.IsDigit(str[i]))
                            sb.Append(str[i]);
                        else
                            break;
                    }
                    str = sb.ToString();

                    x.Add(int.Parse(str));

                }
                await demandControl.RemoveDemand(x);

                ViewControl.frame.Navigate(new PageManageDemand());
            }
        }

        private void btnUpdateDg_Click(object sender, RoutedEventArgs e)
        {
            PageManageDemand page = new PageManageDemand();

            viewControl.AddPageToBackListPages(page);
            ViewControl.frame.Navigate(page);
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int clientId = int.Parse(cmbClient.SelectedItem.ToString().Split(' ')[0]);

                int realtorId = int.Parse(cmbRealtor.SelectedItem.ToString().Split(' ')[0]);

                int estateId = cmbType.SelectedItem.ToString() == "Дом" ? 1 :
                               cmbType.SelectedItem.ToString() == "Квартира" ? 2 : 
                               cmbType.SelectedItem.ToString() == "Земля" ? 3 : 0;

                int? minPrice = parser.ConvertToIntOrNull(tbMinPrice.Text);
                int? maxPrice = parser.ConvertToIntOrNull(tbMaxPrice.Text);

                if(minPrice <0 || maxPrice < 0)
                {
                    MessageBox.Show("Цена не может быть меньше 0");
                    return;
                }
                if(minPrice > maxPrice)
                {
                    MessageBox.Show("Минимальная цена не может превышать максимальную цену");
                    return;
                }

                int? minArea = parser.ConvertToIntOrNull(tbMinArea.Text);
                int? maxArea = parser.ConvertToIntOrNull(tbMaxArea.Text);

                if (minArea < 0 || maxArea < 0)
                {
                    MessageBox.Show("Площадь не может быть меньше 0");
                    return;
                }
                if (minArea > maxArea)
                {
                    MessageBox.Show("Минимальная площадь не может превышать максимальную площадь");
                    return;
                }

                int? minRooms = parser.ConvertToIntOrNull(tbMinRooms.Text);
                int? maxRooms = parser.ConvertToIntOrNull(tbMaxRooms.Text);

                if (minRooms < 1 || maxRooms < 1)
                {
                    MessageBox.Show("Кол-во комнат не может быть меньше 1");
                    return;
                }
                if (minRooms > maxRooms)
                {
                    MessageBox.Show("Минимальное кол-во комнат не может превышать максимальное кол-во комнат");
                    return;
                }

                int? minFloor = parser.ConvertToIntOrNull(tbMinFloors.Text);
                int? maxFloor = parser.ConvertToIntOrNull(tbMaxFloors.Text);

                if (minFloor < 1 || maxFloor < 1)
                {
                    MessageBox.Show("Этаж не должен быть меньше 1");
                    return;
                }
                if (minFloor > maxFloor)
                {
                    MessageBox.Show("Минимальный этаж не может превышать максимальный этаж");
                    return;
                }

                await demandControl.AddDemand(clientId, realtorId, estateId, cmbCity.SelectedItem?.ToString(), cmbStreet.SelectedItem?.ToString(), minPrice, maxPrice, minArea, maxArea, minRooms, maxRooms, minFloor, maxFloor);
                
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить изменения");
            }
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
                var list = await demandControl.GetDemandsAsync();

                dgDemands.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось заполнить список");
            }
        }

        private async Task FillingComboBox()
        {
            try
            {
                var estates = await demandControl.FillingCmbType();

                var realtors = await demandControl.FillingCmbRealtor();

                var clients = await demandControl.FillingCmbClient();

                var cities = await demandControl.FillingCmbCity();

                var streets = await demandControl.FillingCmbStreet();

                cmbClient.ItemsSource = clients;
                cmbRealtor.ItemsSource = realtors;
                cmbType.ItemsSource = estates;
                cmbCity.ItemsSource = cities;
                cmbStreet.ItemsSource = streets;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbType.SelectedItem.ToString() == "Дом")
            {
                spFloors.Visibility = Visibility.Visible;
                spRooms.Visibility = Visibility.Visible;

                labMaxFloor.Content = "Максимальная этажность";
                labMinFloor.Content = "Минимальная этажность";
            }
            else if(cmbType.SelectedItem.ToString() == "Квартира")
            {
                spFloors.Visibility = Visibility.Visible;
                spRooms.Visibility = Visibility.Visible;

                labMaxFloor.Content = "Максимальный этаж";
                labMinFloor.Content = "Минимальный этаж";
            }
            else
            {
                spFloors.Visibility = Visibility.Collapsed;
                spRooms.Visibility = Visibility.Collapsed;
            }
        }
    }
}
