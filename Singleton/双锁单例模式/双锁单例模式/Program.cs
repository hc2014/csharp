using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 双锁单例模式
{
    class Program
    {
        static void Main(string[] args)
        {
            SingletonClass singleton;

            //模拟多个线程创建对象
            //如果没有第二个_instance == null的判断的话,那么singleton对象会被初始化三次,控制台会输出三次 “我被创建了”
            //但是有第二个_instance == null的判断 所以singleton只会被执行一次
            Parallel.For(0, 3, i => {
                singleton = SingletonClass.Instance;
            });


            //借助Lazy<T>来实现 基于线程安全的单例模式
            LazySingleton ls;
            Parallel.For(0, 10, i =>
            {
                ls = LazySingleton.Instance;
            });


            //Lazy 实现延时加载
            //实例化对象，但是此时对象并没有被创建,而且等待对象呗调用的时候才会创建
            Lazy<string> s = new Lazy<string>(TestLazy.GetString);
            //判断对象是否已经被创建,因为没有任何的调用 所以返回的是false
            Console.WriteLine(s.IsValueCreated);
            //调用对象  返回对象的值
            Console.WriteLine(s.Value); 
            //再次判断对象是否已经被创建， 返回是true
            Console.WriteLine(s.IsValueCreated);//返回True 
        }
    }

    /// <summary>
    /// 测试延时加载
    /// </summary>
    public class TestLazy
    {
        public static string GetString()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }


    /// <summary>
    /// 双重锁定单例模式
    /// </summary>
    public class SingletonClass
    {
        private static readonly object _lock = new object();

        //volatile 关键字指示一个字段可以由多个同时执行的线程修改。 声明为 volatile 的字段不受编译器优化（假定由单个线程访问）的限制。
        //这样可以确保该字段在任何时间呈现的都是最新的值。
        private static volatile SingletonClass _instance;

        public static SingletonClass Instance
        {
            get
            {
                //多个线程同时访问，此时_instance==null.然后线程进入到if判断中取
                if (_instance == null)
                {
                    //1.第一个线程先锁定了_lock 对象,其他线程处于等待状态
                    //2.第一个线程释放_lock对象,然后第二个线程马上锁定_lock对象,其他线程继续等待.依次类推执行
                    lock (_lock)
                    {
                        //释放_lock对象后的线程进入到这里,第一个到达这里的线程_instance肯定是null，然后该线程正常执行
                        //问题来了:第二个进入到这里的线程,如果没有_instance == null的判断,那么该线程又会再一次执行new SingletonClass()操作
                        //这就是双锁模式存在的意义
                        if (_instance == null)
                        {
                            _instance = new SingletonClass();
                        }
                    }
                }
                return _instance;
            }
        }

        private SingletonClass()
        {
            Console.WriteLine("我被创建了");
        }
    }


    /// <summary>
    /// 借助Lazy<T>来实现 基于线程安全的单例模式
    /// </summary>
    public class LazySingleton
    {

        private static readonly Lazy<LazySingleton> _instance = new Lazy<LazySingleton>(() => new LazySingleton());

        private LazySingleton()
        {
            Console.WriteLine("我被基于Lazy<T>方式的单例模式创建了");
        }

        public static LazySingleton Instance { get { return _instance.Value; } }

    }
}
