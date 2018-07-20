using System;
namespace ObserverInNET
{
    class Program
    {
        // 委托充当订阅者接口类
        public delegate void NotifyEventHandler(object sender);

        // 抽象订阅号类
        public class Blog
        {
            public NotifyEventHandler NotifyEvent;
            public string Symbol { get; set; }//描写订阅号的相关信息
            public string Info { get; set; }//描写此次update的信息
            public Blog(string symbol, string info)
            {
                this.Symbol = symbol;
                this.Info = info;
            }

            #region 新增对订阅号列表的维护操作
            public void AddObserver(NotifyEventHandler ob)
            {
                NotifyEvent += ob;
            }
            public void RemoveObserver(NotifyEventHandler ob)
            {
                NotifyEvent -= ob;
            }

            #endregion

            public void Update()
            {
                if (NotifyEvent != null)
                {
                    NotifyEvent(this);
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

        // 具体订阅者类
        public class Subscriber
        {
            public string Name { get; set; }
            public Subscriber(string name)
            {
                this.Name = name;
            }

            public void Receive(Object obj)
            {
                Blog xmfdsh = obj as Blog;

                if (xmfdsh != null)
                {
                    Console.WriteLine("订阅者 {0} 观察到了{1}{2}", Name, xmfdsh.Symbol, xmfdsh.Info);
                }
            }
        }

        static void Main1(string[] args)
        {
            Blog xmfdsh = new MyBlog("xmfdsh", "发布了一篇新博客");
            Subscriber wnm = new Subscriber("王尼玛");
            Subscriber tmr = new Subscriber("唐马儒");
            Subscriber wmt = new Subscriber("王蜜桃");
            Subscriber anm = new Subscriber("敖尼玛");

            // 添加订阅者
            xmfdsh.AddObserver(new NotifyEventHandler(wnm.Receive));
            xmfdsh.AddObserver(new NotifyEventHandler(tmr.Receive));
            xmfdsh.AddObserver(new NotifyEventHandler(wmt.Receive));
            xmfdsh.AddObserver(new NotifyEventHandler(anm.Receive));

            xmfdsh.Update();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("移除订阅者王尼玛");
            xmfdsh.RemoveObserver(new NotifyEventHandler(wnm.Receive));
            xmfdsh.Update();

            Console.ReadLine();
        }
    }
}