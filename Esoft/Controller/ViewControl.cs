using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.View;

namespace WpfApp1.Controller
{
    internal class ViewControl
    {
        public static Frame frame;

        public static void Convert(string text, out int result, double min = double.MinValue, double max = double.MaxValue)
        {
            
            if(int.TryParse(text, out result))
            {
                result = int.Parse(text);

                if(result <= min ||
                   result >= max)
                {
                    result = (int)min-1;
                    MessageBox.Show("Введены некорректные данные!",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Введены некорректные данные!",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

            

        }

    }
}
