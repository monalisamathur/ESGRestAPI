using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace ESGRESTAPI
{
    public class ESGUtility

    {

        public Boolean validNumber(String num)
        {
            if (Regex.IsMatch(num, @"\d"))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
