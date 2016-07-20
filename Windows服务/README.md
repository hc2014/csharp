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
注意:在SubmitOrder方法中有一个*Thread.Sleep(10000)*这个并非多余,反而是很有用的,后面讲调试的时候会来说明！



