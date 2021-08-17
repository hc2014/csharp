c# 调用第三方dll的时候 发生异常后无法捕获, 尝试过**ThreadException** 跟**SetUnhandledExceptionMode**以及**UnhandledException**都无效

```
 static class Program
    {
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException +=new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
           Application.Run(new Form1());
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            try
            {
                string errorMsg = "Windows窗体线程异常 : \n\n";
                MessageBox.Show(errorMsg + t.Exception.Message + Environment.NewLine + t.Exception.StackTrace);
            }
            catch
            {
                MessageBox.Show("不可恢复的Windows窗体异常，应用程序将退出！");
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "非窗体线程异常 : \n\n";
                MessageBox.Show(errorMsg + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            catch
            {
                MessageBox.Show("不可恢复的非Windows窗体线程异常，应用程序将退出！");
            }
        }
    }
```



```
		[DllImport("kernel32")]
        private static extern void RtlMoveMemory(IntPtr dst, ref byte src, Int32 len);

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte a = 0;
                RtlMoveMemory(IntPtr.Zero, ref a, 1);
                Console.ReadLine();
            }
            catch (Exception ex)
            {

            }
        }
```

在配置文件中添加**legacyUnhandledExceptionPolicy** 也不行

```xml
	<runtime>
		<legacyUnhandledExceptionPolicy enabled="true" />
	</runtime>
```

 以上方法都无法捕获到**尝试读取或写入受保护的内存。这通常指示其他内存已损坏。**

```
RtlMoveMemory(IntPtr.Zero, ref a, 1)//System.AccessViolationException:“尝试读取或写入受保护的内存。这通常指示其他内存已损坏。”
```



最后发现有个叫做**HandleProcessCorruptedStateExceptions**的特性,在对应方法上面加上这个特性，就可以捕获得到错误信息了

```
[HandleProcessCorruptedStateExceptions]
private void button1_Click(object sender, EventArgs e)
{
        //
}
```



