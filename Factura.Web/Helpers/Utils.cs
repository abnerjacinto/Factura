using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factura.Web.Helpers
{
    public class Utils
    {
        public string TwoDigits(object Val)
        {
            double a;
            a = Convert.ToDouble(Val);
            if (a <= 9)
            {
                return "0" + a;
            }
            else
                return Convert.ToString(a);
        }
        public void FormatTime(ref object CreatedDate, ref object CreatedTime)
        {
            DateTime Temp = DateTime.Now;
            string cd = CreatedDate.ToString();
            if (DateTime.TryParse(cd, out Temp) == true)
            {
                CreatedDate = Temp.Year + "-" + TwoDigits(Temp.Month) + "-" + TwoDigits(Temp.Day);
                //CreatedDate = ConvertDate(CreatedDate, 3)
            }
            else
            {
                CreatedDate = DateTime.Now.Year + "-" + TwoDigits(DateTime.Now.Month) + "-" + TwoDigits(DateTime.Now.Day);
            }
            int ct;
            cd = CreatedTime.ToString();
            int.TryParse(cd, out ct);

            if (ct <= 0)
            {
                if (DateTime.Now.Second == 60)
                {
                    CreatedTime = DateTime.Now.Hour + TwoDigits(DateTime.Now.Minute) + TwoDigits(DateTime.Now.Second - 1);
                }
                else
                {
                    CreatedTime = DateTime.Now.Hour + TwoDigits(DateTime.Now.Minute) + TwoDigits(DateTime.Now.Second);
                }
            }
        }
    }
}