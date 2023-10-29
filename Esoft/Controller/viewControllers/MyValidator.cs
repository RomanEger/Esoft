using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Esoft.Controller
{
    internal class MyValidator
    {
        public bool IsValidEmail(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex regex = new Regex("^([\\w.-]+)([@][a-z.-]+)([.]+[a-z]{2,6})$");
                if (regex.IsMatch(str))
                    return true;
            }
            return false;
        }

        public bool IsValidMobileNumber(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex regex = new Regex("^([8])(\\d){10}$");
                if (regex.IsMatch(str))
                    return true;
            }
            return false;
        }
    }
}
