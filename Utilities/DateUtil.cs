using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    public class DateUtil
    {
        private static Random random = new Random();

        // Method to generate a random integer within a specified range
        public static int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max + 1);
        }

        // Method to generate a random string of specified length
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Method to generate a random date within a specified range
        public static DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;
            return startDate.AddDays(random.Next(range));
        }

    }
}
