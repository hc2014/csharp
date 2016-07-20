业务需求,需要做一个定时执行数据库的功能,首先想到的是数据库的定时任务,但是定时任务没有办法传递参数(执行平率).所以最后还是用windows服务来做这个功能!

# 一.创建服务
###1.新建项目-->选择Windows服务。默认生成文件包括Program.cs，Service1.cs
###2.在Service1.cs添加如下代码：
```
        protected override void OnStart(string[] args)
        {
            //定时器的子线程
            Task queryTask = new Task(QueryOriginal);
            queryTask.Start();
            
            //其他一些执行代码
            Task task = new Task(SubmitOrder);
            task.Start();


        }
        
        public void QueryOriginal()
        {
            timer1 = new System.Timers.Timer();
            timer1.Interval = 1000;  //设置计时器事件间隔执行时间
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
        }
        
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
          //这里用来写 需要定时去执行的代码
        }
        
        
        public void SubmitOrder()
        {
            //Thread.Sleep(10000);
            //这里用来执行其他一些代码
        }
        
```
注意:在SubmitOrder方法中有一个**Thread.Sleep(10000)**这个并非多余,反而是很有用的,后面讲调试的时候会来说明！



#二.添加window服务安装程序
###1.打开Service1.cs【设计】页面，点击右键，选择【添加安装程序】，会出现serviceInstaller1和serviceProcessInstaller1两个组件
###2.将serviceProcessInstaller1的Account属性设为【LocalSystem】.
###3.serviceInstaller1的StartType属性设为【Automatic】，ServiceName属性设置服务名称，此后在【管理工具】--》【服务】中即显示此名称


#三.安装、卸载window服务
###1.输入cmd(命令行)
 4.0框架：cd C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319
 2.0框架：cd C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
###2.安装服务（项目生成的exe文件路径）
InstallUtil C:\Users\hc\Desktop\工作目录\esAPI\DotNet.CommonV4.1\PushTradeService\bin\Debug\PushTradeService.exe
###3.卸载服务
InstallUtil /u C:\Users\hc\Desktop\工作目录\esAPI\DotNet.CommonV4.1\PushTradeService\bin\Debug\PushTradeService.exe

注意:这个时候执行可能会报一个错误:**System.Security.SecurityException: 未找到源,但未能搜索某些或全部事件日志。**

碰见这个错误的话,右键项目->管理员权限.去执行就可以了.但是我自己这边的源代码(Visual Stadio)右键菜单,没有"管理员权限"这个菜单！
所以我是把上面的命令写成一个.bat(批处理文件).这样问题就解决了！

#四.查看window服务
###1.开始菜单->运行->services.msc
###2.控制面板-->管理工具-->服务
可以在服务里面手动开启、停止服务,当然也是可以写命令来的

>开启:net statr PushTradeService
>停止:net stop PushTradeService
然后把上面2句话分别保存成为bat文件即可!


#五.调试window服务

