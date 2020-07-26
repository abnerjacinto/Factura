using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factura.Web.Helpers
{
    public class Utils
    {
        public string SetDate()
        {
            return DateTime.Now.Year.ToString() + "-" + TwoDigits(DateTime.Now.Month) + "-" + TwoDigits(DateTime.Now.Day);
        }
        public double SetTime()
        {
            string CreatedTime;
            if (DateTime.Now.Second == 60)
            {
                CreatedTime = DateTime.Now.Hour.ToString() + TwoDigits(DateTime.Now.Minute) + TwoDigits(DateTime.Now.Second - 1);
            }
            else
            {
                CreatedTime = DateTime.Now.Hour.ToString() + TwoDigits(DateTime.Now.Minute) + TwoDigits(DateTime.Now.Second);
            }
            return Double.Parse(CreatedTime);
        }
        public string ConvertDate(string Data, int Format)
        {
            string ConvertDate = "";
            DateTime Temp;
            string cd = Data.ToString();
            DateTime.TryParse(cd, out Temp);
            if (Data.ToString().Length > 0)
            {
                switch (Format)
                {
                    case 1: //formato dd/mm/yyyy
                        if (Temp.Day < 13)
                            ConvertDate = TwoDigits(Temp.Month) + "-" + TwoDigits(Temp.Day) + "-" + Temp.Year;
                        else
                            ConvertDate = TwoDigits(Temp.Day) + "-" + TwoDigits(Temp.Month) + "-" + Temp.Year;
                        break;
                    case 2://formato yyyy/mm/dd para strings
                        ConvertDate = Temp.Year + "-" + TwoDigits(Temp.Month) + "-" + TwoDigits(Temp.Day);
                        break;
                    case 3://yyyy/mm/dd para dates
                        ConvertDate = Temp.Year + "-" + TwoDigits(Temp.Month) + "-" + TwoDigits(Temp.Day);
                        break;
                    case 4:
                        if (Temp.Day < 13)
                            ConvertDate = TwoDigits(Temp.Month) + " de " + TwoDigits(Temp.Day) + " de " + Temp.Year;
                        else
                            ConvertDate = TwoDigits(Temp.Day) + " de " + TwoDigits(Temp.Month) + " de " + Temp.Year;
                        break;
                    case 5://mm/dd/yyyy
                        ConvertDate = TwoDigits(Temp.Day) + "-" + TwoDigits(Temp.Month) + "-" + Temp.Year;
                        break;
                    case 6://dd/mm/yyyy para dates
                        ConvertDate = TwoDigits(Temp.Day) + "-" + TwoDigits(Temp.Month) + "-" + Temp.Year;
                        break;

                }

            }
            return ConvertDate;

        }
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