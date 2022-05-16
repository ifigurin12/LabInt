using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lab.Classes
{// 2x - ln(11x) - 1 
    public class TrapezoidalMethod : ICalculator
    {
        double ICalculator.Calculate(int SplitNumbers, double UpLim, double LowLim, Func<double, double> integral, out double time)
        {
            if (SplitNumbers <= 0)
            {
                throw new ArgumentException();
            }
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

        double ICalculator.CalculateParallel(int SplitNumbers, double UpLim, double LowLim, Func<double, double> integral, out double time)
        {
            if (SplitNumbers <= 0)
            {
                throw new ArgumentException();
            }
            Stopwatch tn = new Stopwatch();
            tn.Start();

            double h = (UpLim - LowLim) / SplitNumbers;
            double sum = 0.0;
            object barrier = new object();

            Parallel.For(0, SplitNumbers,
                () => 0.0,
                (i, state, localTotal) => localTotal + integral(LowLim+ h * i),
                localTotal => { lock (barrier) { sum += localTotal; } });
            sum += (integral(UpLim) + integral(LowLim)) / 2;
            
            tn.Stop();
            TimeSpan tk = tn.Elapsed;
            time = tk.TotalMilliseconds;
            return h * ((integral(LowLim) + integral(UpLim)) / 2 + sum);
        }
    }
}
