# ObservableCollectionstring<T>

工作中如果想根据某个集合、列表里面的数据发生了变化去处理其他事情，那就需要定时去轮训，这样不太好。

c#现在有 `ObservableCollectionstring<T>`这种可观察集合

其核心内容就是定义了一个`NotifyCollectionChangedEventHandler`的事件，这个事件可以监听到 集合的变化

```c#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<string> list = new ObservableCollection<string>() { "1" };

            list.CollectionChanged += list_CollectionChanged;

            for (int i = 0; i < 1000; i++)
            {
                if (i % 3 == 1)
                {
                    list.RemoveAt(0);
                }
                else
                {
                    list.Add(i.ToString());
                }
            }

            Console.WriteLine("全部结束！！！");

            Console.Read();
        }
        static void list_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //为了不阻止主线程Add，事件用 “工作线程”处理
            Task.Factory.StartNew((o) =>
            {
                var obj = o as NotifyCollectionChangedEventArgs;

                switch (obj.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Console.WriteLine("当前线程:{0}, 操作是:{1} 数据:{2}", Thread.CurrentThread.ManagedThreadId, obj.Action.ToString(), obj.NewItems[0]);
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Console.WriteLine("当前线程:{0}, 操作是:{1} 数据:{2}", Thread.CurrentThread.ManagedThreadId, obj.Action.ToString(), obj.OldItems[0]);
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        break;
                    default:
                        break;
                }

                Thread.Sleep(1000);
            }, e);
        }

    }
}
```

执行看看，很好用的