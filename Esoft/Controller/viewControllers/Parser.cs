using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Esoft.Controller
{
    internal class Parser
    {
        public int? ConvertToIntOrNull(string text, int min = int.MinValue, int max = int.MaxValue)
        {
            if(text == null) return null;
            if (int.TryParse(text, out int result))
            {
                result = int.Parse(text);

                if (result <= min ||
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

        public double? ConvertToDoubleOrNull(string text, double min = double.MinValue, double max = double.MaxValue)
        {
            if(text == null) return null;   
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

        public string GetStringOrNullData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            else
                return data;
        }

    }
}
