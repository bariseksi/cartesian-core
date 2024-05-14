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

        private static int power = 0;
        public static double GetStep(double value, SnapTo snapTo)
        {
            if (value == 0)
                return 1;

            double[] steps = { 1, 2, 5 };
            int stepArraySize = steps.Length;
            power = 0;
            double val = value;

            if (val < 1)
            {
                while (true)
                {
                    val *= 10;
                    power--;
                    if (val >= 1)
                        break;

                }
            }
            else
            {
                while (true)
                {
                    val /= 10;
                    if (val < 1)
                        break;
                    power++;
                }
            }

            var fraction = Math.Pow(10, power);

            int index = snapTo == SnapTo.Ceil ? 0 : steps.Length * 2 - 1;
            while (true)
            {
                var step = steps[index % steps.Length] * fraction;
                if (snapTo == SnapTo.Ceil)
                {
                    if (value <= step)
                        return step;

                    index++;

                    if (index % steps.Length == 0) //we couldnt find the ceil then increase the fraction times 10;
                    {
                        fraction *= 10;
                    }
                }
                else
                {
                    if (value >= step)
                        return step;

                    if (index % steps.Length == 0) //we couldnt find the ceil then increase the fraction times 10;
                    {
                        fraction /= 10;
                    }
                    index--;
                }


            }
        }

        public static double FindMax(double value, double step)
        {
            if (value < 0)
                return FindMin(value * -1, step) * -1;
            return ((int)(value / step) + 1) * step;
        }

        public static double FindMin(double value, double step)
        {
            if (value < 0)
                return FindMax(value * -1, step) * -1;
            return ((int)(value / step)) * step;
        }
    }

    public enum SnapTo
    {
        Ceil,
        Floor
    }
}
