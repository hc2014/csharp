using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    //贴上方法标签
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class MyCalculatorMethodAttribute:Attribute
    {
        public MyCalculatorMethodAttribute()
        {
            Console.WriteLine("MyCalculatorMethodAttribute...构造函数");
        }

    }
}
