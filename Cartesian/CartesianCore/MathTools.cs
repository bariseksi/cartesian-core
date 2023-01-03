using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianCore
{
    public static class MathTools
    {
        public static double Floor(double value, double number = 1)
        {
            if (number < 0)
                number *= -1;

            double remainder = FindRemainder(value, number);
            if (remainder < 0)
                return ((-value + remainder) + number) * -1;

            return value - remainder;
        }

        public static double Ceil(double value, double number = 1)
        {
            if (number < 0)
                number *= -1;

            double remainder = FindRemainder(value, number);
            if (remainder <= 0)
                return value - remainder;

            return value - remainder + number;
        }

        private static double FindRemainder(double dividend, double divisor)
        {
            int quotient = (int)(dividend / divisor);
            return dividend - (quotient * divisor);
        }
    }
}
