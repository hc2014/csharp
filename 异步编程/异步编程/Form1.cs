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
                Thread.Sleep(1000);
                return "没有阻塞...\r\n";
            });
        }

    }
}
