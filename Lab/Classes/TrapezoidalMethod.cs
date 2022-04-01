using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Lab.Classes
{// 2x - ln(11x) - 1 
    public class TrapezoidalMethod : ICalculator
    {
        double ICalculator.Calculate(double SplitNumbers, double UpLim, double LowLim, Func<double, double> integral, out double time)
        {
            Stopwatch tn = new Stopwatch();
            tn.Start();

            double h = (UpLim - LowLim) / SplitNumbers;
            double sum = 0.0; 

            for (int i = 0; i < SplitNumbers; i++)
            {
                sum += integral(LowLim + h * i); 
            }
            tn.Stop();
            TimeSpan tk = tn.Elapsed;
            time = tk.TotalMilliseconds;
            return h * ((integral(LowLim) + integral(UpLim)) / 2 + sum); 
        }
    }
}
