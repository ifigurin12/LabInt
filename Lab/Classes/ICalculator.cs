using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Classes
{
    public interface ICalculator
    {
        public double Calculate(double SplitNumbers, double UpLim, double LowLim, Func<double, double> integral, out double time);
    }
}
