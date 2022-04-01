using System;
using Xunit;
using Lab.Classes;
using Lab; 

namespace LabTest
{
    public class MethodsTest
    {
        [Fact]
        public void SimpsonMethodTest()
        {
            // arrange 
            ICalculator integral = new SimsonMethod();
            double expected = 990695.748;
            // act 
            double time;
            double actual = Math.Round(integral.Calculate(1000, 1000, 1, x => (2 * x) - Math.Log(11 * x) - 1, out time), 3);
            // assert 
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TrapezoidalMethodTest()
        {
            // arrange 
            ICalculator integral = new TrapezoidalMethod();
            double expected = 990694.432;
            // act 
            double time;
            double actual = Math.Round(integral.Calculate(1000, 1000, 1, x => (2 * x) - Math.Log(11 * x) - 1, out time), 3);
            // assert 
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void EqTrapezoidalNSimpson()
        {
            // arrange 
            ICalculator integralTrapez = new TrapezoidalMethod();
            ICalculator integralSimp = new SimsonMethod();
            // act 
            double time;
            double firstMethod = Math.Round(integralTrapez.Calculate(1000, 1000, 1, x => (2 * x) - Math.Log(11 * x) - 1, out time), 3);
            double secondMethod = Math.Round(integralSimp.Calculate(1000, 1000, 1, x => (2 * x) - Math.Log(11 * x) - 1, out time), 3);
            // assert 
            Assert.NotEqual(firstMethod, secondMethod);
        }
        [Fact]
        public void EqSimpsonNTrapezoidal()
        {
            // arrange 
            ICalculator integralTrapez = new TrapezoidalMethod();
            ICalculator integralSimp = new SimsonMethod();
            // act 
            double time;
            double firstMethod = Math.Round(integralTrapez.Calculate(1000, 1000, 1, x => (2 * x) - Math.Log(11 * x) - 1, out time), 3);
            double secondMethod = Math.Round(integralSimp.Calculate(1000, 1000, 1, x => (2 * x) - Math.Log(11 * x) - 1, out time), 3);
            // assert 
            Assert.NotEqual(secondMethod, firstMethod);
        }
    }
}
