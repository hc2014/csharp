using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 显示调用接口Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass c = new MyClass();

            int i = c.Add(3);
            Console.WriteLine("调用基类的Add方法结果是:"+i);

            i = ((addInterface)c).Add(3);
            Console.WriteLine("显示调用接口的Add方法结果是:" + i);
        }
    }

    interface addInterface
    {
        int Add(int i);
    }

    class BaseClass
    {
        public int Add(int i)
        {
            return i + 1;
        }
    }
    

    class MyClass :BaseClass, addInterface
    {
        int addInterface.Add(int i)
        {
            return i + i;
        }
    }
}
