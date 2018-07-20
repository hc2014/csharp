using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    // 订阅号抽象类
    public abstract class Blog
    {
        // 保存订阅者列表
        private List<IObserver> observers = new List<IObserver>();

        public string Symbol { get; set; }//描写订阅号的相关信息
        public string Info { get; set; }//描写此次update的信息
        public Blog(string symbol, string info)
        {
            this.Symbol = symbol;
            this.Info = info;
        }

        // 对同一个订阅号，新增和删除订阅者的操作
        public void AddObserver(IObserver ob)
        {
            observers.Add(ob);
        }
        public void RemoveObserver(IObserver ob)
        {
            observers.Remove(ob);
        }

        public void Update()
        {
            // 遍历订阅者列表进行通知
            foreach (IObserver ob in observers)
            {
                if (ob != null)
                {
                    ob.Receive(this);
                }
            }
        }
    }

    // 具体订阅号类
    public class MyBlog : Blog
    {
        public MyBlog(string symbol, string info)
            : base(symbol, info)
        {
        }
    }

    // 订阅者接口
    public interface IObserver
    {
        void Receive(Blog tenxun);
    }

    // 具体的订阅者类
    public class Subscriber : IObserver
    {
        public string Name { get; set; }
        public Subscriber(string name)
        {
            this.Name = name;
        }

        public void Receive(Blog xmfdsh)
        {
            Console.WriteLine("订阅者 {0} 观察到了{1}{2}", Name, xmfdsh.Symbol, xmfdsh.Info);
        }
    }

    // 客户端测试
    class Program
    {
        static void Main(string[] args)
        {
            Blog xmfdsh = new MyBlog("xmfdsh", "发布了一篇新博客");

            // 添加订阅者
            xmfdsh.AddObserver(new Subscriber("王尼玛"));
            xmfdsh.AddObserver(new Subscriber("唐马儒"));
            xmfdsh.AddObserver(new Subscriber("王蜜桃"));
            xmfdsh.AddObserver(new Subscriber("敖尼玛"));

            //更新信息
            xmfdsh.Update();
            //输出结果，此时所有的订阅者都已经得到博客的新消息
            Console.ReadLine();
        }
    }
}
