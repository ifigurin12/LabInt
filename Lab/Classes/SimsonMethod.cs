using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Lab.Classes
{  // 2x - ln(11x) - 1 
    public class SimsonMethod : ICalculator
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
                sum += integral(LowLim + h * i) + 2 * integral(LowLim + i * h + h / 2);
            }
            tn.Stop();
            TimeSpan tk = tn.Elapsed; 
            time = tk.TotalMilliseconds;
            return h / 3 * ((integral(UpLim) - integral(LowLim)) / 2 + sum); 
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
                (i, state, localTotal) => localTotal + integral(LowLim + h * i) + 2 * integral(LowLim + i * h + h / 2),
                localTotal => { lock (barrier) { sum += localTotal; } });
            sum += (integral(UpLim) + integral(LowLim)) / 2;

            tn.Stop();
            TimeSpan tk = tn.Elapsed;
            time = tk.TotalMilliseconds;
            return h / 3 * ((integral(UpLim) - integral(LowLim)) / 2 + sum);
        }
    }
}
