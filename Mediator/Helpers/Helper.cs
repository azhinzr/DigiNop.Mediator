using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mediator.Helpers
{
    public static class Helper
    {
        public static string GetPersianDate(DateTime date)
        {
            var calendar = new PersianCalendar();
            int daysofMonth = DateTime.DaysInMonth(calendar.GetYear(date), calendar.GetMonth(date));
            int day = calendar.GetDayOfMonth(date) > daysofMonth ? daysofMonth : calendar.GetDayOfMonth(date);
            var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), day);
            var result = persianDate.ToString("yyyyMMdd");
            return result;
        }
        public static string ToEnglishNumber(string input)
        {
            var regex = new Regex("[a-zA-Z0-9 ]*");
            var result = input.Split(',')
                .Where(s => regex.Match(s).Value == s)
                .ToArray();
            if (result.Any())
            {
                return input;
            }
            var englishNumbers = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    englishNumbers += char.GetNumericValue(input, i);
                }
                else
                {
                    englishNumbers += input[i].ToString();
                }
            }
            return englishNumbers;
        }

        public static double ToTomanPrice(double price)
        {
            return price * 10;
        }
        public static double ToDigiPrice(double price)
        {
            var tmp = (price % 100).ToString(CultureInfo.InvariantCulture);
            if (tmp == "0") return price;
            var result = price.ToString(CultureInfo.InvariantCulture).Replace(tmp, "00");
            return Convert.ToDouble(result);
        }
    }
}
