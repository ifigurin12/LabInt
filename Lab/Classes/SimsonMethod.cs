using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Lab.Classes
{  // 2x - ln(11x) - 1 
    class SimsonMethod : ICalculator
    {
        double ICalculator.Calculate(double SplitNumbers, double UpLim, double LowLim, Func<double, double> integral, out double time)
        {
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
    }
}
