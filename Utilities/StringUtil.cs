using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    public class StringUtil
    {
        // Method to truncate a string to a specified length
        public static string Truncate(string input, int maxLength)
        {
            if (input.Length > maxLength)
                return input.Substring(0, maxLength);
            return input;
        }

        // Method to check if a string is null or empty
        public static bool IsNullOrEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        // Method to reverse a string
        public static string Reverse(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
