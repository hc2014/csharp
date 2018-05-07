using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Calculator;
using System.Runtime.Remoting.Contexts;

namespace AopAttributeCalculatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例化Calculator类
            Calculator.Calculator calculator = new Calculator.Calculator();
            calculator.X = 10;
            calculator.Y = 20;

            CalculatorHandler handler = new CalculatorHandler();
            //贴上标签的方法
            handler.Add(calculator.X, calculator.Y);
            handler.substract(calculator);
            //没有贴上标签的方法
            handler.Mutiply(30, 40);
            handler.Divide(calculator);

            //Communication直接继承 ContextBoundObject，这个类更简洁
            //实例化Communition类
            Communication com = new Communication();
            com.SayBye();
            com.SayHello();

            Console.ReadKey();
        }      
    }

    //贴上类的标签
    [MyCalculator]
    //一定要继承ContextBoundObject类
    public class Communication:ContextBoundObject
    {
        public Communication()
        { }
        //贴上方法标签
        [MyCalculatorMethod]
        public void SayHello()
        {
            Console.WriteLine("hello...");
        }

        public void SayBye()
        {
            Console.WriteLine("bye!");
        }
    }

}
