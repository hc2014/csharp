using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 异步编程
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //按道理来说,不管点击了button1还是button2界面都应该处于假死的状态,但是用了async跟await的话就不会发生阻塞
        //点击完button1后可以继续点击button2，然后等待事件执行完成以后,文本框上回显示两个事件返回的数据


        /// <summary>
        /// 应用一
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int length = await ExampleMethodAsync();

                textBox1.Text += String.Format("Length: {0}\r\n", length);
            }
            catch (Exception)
            {
                
            }

        }

        public async Task<int> ExampleMethodAsync()
        {
            var httpClient = new HttpClient();
            int exampleInt = (await httpClient.GetStringAsync("http://www.baidu.com")).Length;
            textBox1.Text += "Preparing to finish ExampleMethodAsync.\\n";
            Thread.Sleep(5000);

            return exampleInt;
        }



        /// <summary>
        /// 应用二
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += (await GetString());
        }

        async Task<string> GetString()
        {
            return  await Task.Run(() => {
                Thread.Sleep(5000);
                return "没有阻塞...\r\n";
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mydelegate = new MyDelegate(TestMethod);
            IAsyncResult result = mydelegate.BeginInvoke("Thread Param", TestCallback, null);
            //判断是否执行完成
            textBox1.Text = "请稍等";
            //while (!result.AsyncWaitHandle.WaitOne(1000))
            //{
            //    textBox1.Text += ".";
            //}
        }

        public delegate string MyDelegate(object data);
        private MyDelegate mydelegate = null;

        public string TestMethod(object data)
        {
            string datastr = data.ToString();
            System.Threading.Thread.Sleep(5000);
            return datastr;
        }

        //异步回调函数，异步执行完成以后需要返回的数据
        public void TestCallback(IAsyncResult data)
        {
            //datastr,异步回调最终得到的数据，也就是异步执行程序执行完了以后的返回值
            //data,异步回调的对象,也是传入回调函数的一个数据,可以用来和返回值进行相关操作
            string datastr = mydelegate.EndInvoke(data);

            textBox1.BeginInvoke(new MethodInvoker(()=> {
                textBox1.Text += "\r\n" + datastr;
            }));
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //直接 await 会阻塞后面的代码执行
            string urlContents1 = await Work();

            //赋值给一个Task，然后wait task 是不会阻塞  var a=1的执行
            Task<string> getStringTask = Work();
            var a = 1;

            string urlContents = await getStringTask;

            
        }

        Task<string> Work()
        {
            return   Task.Run(() => {
                Thread.Sleep(5000);
                return "OK";
            });
        }
    }
}
