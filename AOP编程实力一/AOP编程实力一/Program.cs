using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP编程实力一
{
    class Program
    {
        static void Main(string[] args)
        {
            //此方法实现方式最大的问题在于 实例化对象必须要用动态类型(dynamic)
            dynamic test = new TestClass();
            try
            {
                test.TestMethodReturn();
                test.TestMethodArguments("aaaaa", 123);
                test.TestMethodException();
            }
            catch (Exception)
            {
               
            }
            Console.Read();
        }
    }
}
