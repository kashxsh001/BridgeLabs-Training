using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class StringUtils
    {
        public string Reverse(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public bool IsPalindrome(string str)
        {
            return str == Reverse(str);
        }

        public string ToUpperCase(string str)
        {
            return str.ToUpper();
        }
    }
}
