using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    //业务层的类和方法，让他继承自上下文绑定类的基类
    [MyCalculator]
    public class CalculatorHandler:ContextBoundObject,ICalculator
    {
        //具备标签的方法才能被拦截
        [MyCalculatorMethod]
        public double Add(double x, double y)
        {
            Console.WriteLine("{0} + {1} = {2}", x, y, x + y);
            return x + y;
        }

        [MyCalculatorMethod]
        public double substract(Calculator calculator)
        {
            Console.WriteLine("{0} - {1} = {2}",calculator.X,calculator.Y,calculator.X - calculator.Y);
            return calculator.X - calculator.Y;
        }

        public double Mutiply(double x, double y)
        {
            Console.WriteLine("{0} * {1} = {2}", x, y, x * y);
            return x * y;
        }

        public double Divide(Calculator calculator)
        {
            Console.WriteLine("{0} / {1} = {2}", calculator.X, calculator.Y, calculator.X / calculator.Y);
            return calculator.X / calculator.Y;
        }
    }
}
