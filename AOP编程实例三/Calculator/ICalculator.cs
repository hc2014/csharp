using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public interface ICalculator
    {
        double Add(double x, double y);

        double substract(Calculator calculator);

        double Mutiply(double x, double y);

        double Divide(Calculator calculator);
    }
}
