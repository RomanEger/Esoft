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

        public static string GetStringOrNullData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            else
                return data;
        }

        public static bool IsValidEmail(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex regex = new Regex("^([\\w.-]+)([@][a-z.-]+)([.]+[a-z]{2,6})$");
                if(regex.IsMatch(str))
                    return true;
            }
            return false;
        }

        public static bool IsValidMobileNumber(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex regex = new Regex("^([8])(\\d){10}$");
                if (regex.IsMatch(str))
                    return true;
            }
            return false;
        }


        //str = str.Replace(" ", string.Empty);

        //        for (int j = 0; j<str.Length; j++)
        //            if (!char.IsDigit(str[j]))
        //                str = str.Remove(j, 1);

    }
}
