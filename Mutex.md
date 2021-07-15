# Mutex

[Msdn上面对Mutex类的定义](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.mutex?view=netframework-4.8)

```c#
static string mutexName = "TestMutex";

static void Main(string[] args)
{
    using (var m = new Mutex(false, mutexName))
    {
        if (!m.WaitOne(TimeSpan.FromSeconds(5), false))
        {
            Console.WriteLine("second is running");
        }
        else
        {
            Console.WriteLine("Running");
            Console.ReadLine();
            m.ReleaseMutex();
        }
    }

    Console.ReadLine();
}
```



Mutex可以在进程与线程中来控制资源的访问权限

 第一个控制台程序运行时，m是有信号的，因此m.WaitOne(TimeSpan.FromSeconds(5),false)会立即返回true，if的测试条件为false（因为!true为false），会执行else部分，同时m变成无信号；
第二个控制台程序运行时，因为m是命名互斥体对象（可用于系统中的进程或线程同步），如果第一个控制台程序还没有运行过m.ReleaseMutex();语句（作用是使m变成有信号），则第二个控制台程序执行m.WaitOne(TimeSpan.FromSeconds(5),false)时，会阻塞5秒以等待m变成有信号，如果第一个控制台程序没有在5秒内执行m.ReleaseMutex();语句，则第二个控制台程序会因等不到信号，导致m.WaitOne(TimeSpan.FromSeconds(5),false)5秒超时返回，会返回false，因此第二个控制台程序会执行if部分（因为!false为true）。 





