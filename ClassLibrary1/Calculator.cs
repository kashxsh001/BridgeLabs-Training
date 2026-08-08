using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Calculator
    {
        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new ArithmeticException("Cannot divide by zero");

            return a / b;
        }
    }
}
