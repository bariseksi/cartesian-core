using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianCore
{
    public class Scale
    {
        private readonly double valueMin = 0;
        private readonly double scaleMin = 0;
        private readonly double diffValue = 0;
        private readonly double diffScale = 0;
        public Scale(double valueMin, double valueMax, double scaleMin = 0, double scaleMax = 100)
        {
            this.valueMin = valueMin;
            this.scaleMin = scaleMin;
            diffValue = valueMax - valueMin;
            diffScale = scaleMax - scaleMin;
        }

        public double Calculate(double value)
        {
            return ((value - valueMin) * (diffScale / diffValue)) + scaleMin;
        }
    }
}
