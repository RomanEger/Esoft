using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WpfApp1.View;

namespace WpfApp1.Controller
{
    internal class ViewControl
    {
        public static Frame frame;

        public static int? ConvertToIntOrNull(string text, int min = int.MinValue, int max = int.MaxValue)
        {
            
            if(int.TryParse(text, out int result))
            {
                result = int.Parse(text);

                if(result <= min ||
                   result >= max)
                {
                    MessageBox.Show("Введены некорректные данные!",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                    return null;
                }
                return result;
            }
            else return null;
        }

        public static double? ConvertToDoubleOrNull(string text, double min = double.MinValue, double max = double.MaxValue)
        {
            text = text.Replace('.', ',');
            if (double.TryParse(text, out double result))
            {
                result = double.Parse(text);

                if (result <= min ||
                   result >= max)
                {
                    MessageBox.Show("Некорректные данные!",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                    return null;
                }
                return result;
            }
            else return null;
        }

    }
}
