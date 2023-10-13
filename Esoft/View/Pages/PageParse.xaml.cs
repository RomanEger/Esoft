using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp1.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageParce.xaml
    /// </summary>
    public partial class PageParse : Page
    {
        ModelControl modelControl;

        public PageParse()
        {
            InitializeComponent();
            modelControl = new ModelControl();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string[] filedata;
            string[][] data;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                filedata = File.ReadAllLines(openFileDialog.FileName, Encoding.GetEncoding(1251));
                data = new string[filedata.Length][];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = filedata[i].Split(',');
                }
                modelControl.AddData(data, 0);
            }
            
        }
    }
}
