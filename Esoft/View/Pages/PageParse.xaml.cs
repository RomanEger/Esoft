using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp1.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageParce.xaml
    /// </summary>
    public partial class PageParse : Page
    {
        ModelControl modelControl;

        string[] filedata;
        string[][] data;

        Regex regexTxt;
        Regex regexCsv;

        public PageParse()
        {
            InitializeComponent();

            modelControl = new ModelControl();

            cmbFile.ItemsSource = new List<string>() { "Дом", "Квартира", "Земля"};

            regexTxt = new Regex(@"\w*.txt$");
            regexCsv = new Regex(@"\w*.csv$");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog() == true)
            {
                MatchCollection matchesTxt = regexTxt.Matches(openFileDialog.FileName);
                MatchCollection matchesCsv = regexCsv.Matches(openFileDialog.FileName);

                if (matchesTxt.Count > 0)
                {
                    filedata = File.ReadAllLines(openFileDialog.FileName, Encoding.GetEncoding(1251));

                    data = new string[filedata.Length][];

                    for (int i = 0; i < data.Length; i++)
                        data[i] = filedata[i].Split(',');
                }
                else if(matchesCsv.Count > 0)
                {
                    filedata = File.ReadAllLines(openFileDialog.FileName, Encoding.UTF8);

                    data = new string[filedata.Length][];

                    for (int i = 0; i < data.Length; i++)
                        data[i] = filedata[i].Split(',');
                }
                else
                    MessageBox.Show("Выберите файл формата \".txt\" или \".csv\"");
                //modelControl.AddEstate(data, cmbFile.SelectedIndex);
                labelOpenFile.Content = "Открыт файл: " +openFileDialog.SafeFileName;
            }
            
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("Выберите файл");
            }
            else if ((cmbFile.SelectedItem != null || rbOffer.IsChecked == true) &&
                     data.Length > 0)
            {
                if (rbEstate.IsChecked == true)
                    await modelControl.AddEstate(data, cmbFile.SelectedIndex);
                else if (rbOffer.IsChecked == true)
                    await modelControl.AddOffer(data);
                else if (rbDemand.IsChecked == true)
                    await modelControl.AddDemand(data, cmbFile.SelectedIndex);
                else
                    MessageBox.Show("Выберите таблицу");
            }
            else if (data.Length == 0)
            {
                MessageBox.Show("Файл не содержит данных");
            }
            else if (cmbFile.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип недвижимости");
            }

        }


        private void rbOffer_Checked(object sender, RoutedEventArgs e)
        {
            cmbFile.IsEnabled = false;
        }

        private void rbOffer_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbFile.IsEnabled = true;
        }
    }
}
