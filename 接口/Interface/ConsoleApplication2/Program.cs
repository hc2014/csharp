using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //People类继承了Animal类  跟ISound接口跟IWork接口，但是People类没有任何的实现
            //那是因为积累Animal已经有了ISound接口跟IWork接口方法里面必须要去实现的方法了
            People p = new People();           
            p.say();
            p.run();

            //如果People也实现了run方法,那么编译器会提示说，隐藏了基类的方法，如果有意隐藏请用new关键字
        }
    }

    public interface IWork
    {
        void run();
    }

    public interface ISound
    {
        void say();
    }

    class Animal
    {
        public void say()
        {
            Console.WriteLine("我是个动物");
        }

        public void run()
        {
            Console.WriteLine("我是个动物,我正在奔跑");
        }
    }

    class People : Animal, ISound, IWork
    {
        //public void run()
        //{
        //    Console.WriteLine("我是个人,我正在奔跑");
        //}
    }
}
