using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Unit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //自己创建的3个类，基本一样，而事实上肯定不会是一样的，
            //这里写很多重复代码，主要就是为了模仿加载不同dll完整的处理流程
            switch (comboBox1.Text)
            {
                case"天空":
                     Assembly objAss = Assembly.LoadFrom(@"E:\hc\新建文件夹\ReflectionCode\插件机制\bin\Debug\Fly.dll");

                     Type t = objAss.GetType("Fly.Fly");

                    //动态生成类StringUtil的实例   
                    object obj = System.Activator.CreateInstance(t);

                    //找到对应的方法   
                    MethodInfo p = t.GetMethod("GetMsg");

                    object objRetval = p.Invoke(obj, null);
                    richTextBox1.Text += objRetval.ToString();
                    break;
                case "大海":
                    Assembly objAss2 = Assembly.LoadFrom(@"E:\hc\新建文件夹\ReflectionCode\插件机制\bin\Debug\Swam.dll");

                    Type t2 = objAss2.GetType("Swam.Swam");

                    //动态生成类StringUtil的实例   
                    object obj2 = System.Activator.CreateInstance(t2);

                    //找到对应的方法   
                    MethodInfo p2 = t2.GetMethod("GetMsg");

                    object objRetval2 = p2.Invoke(obj2, null);
                    richTextBox1.Text += objRetval2.ToString();

                    break;
                case "大地":
                    //dll的路径
                    Assembly objAss3 = Assembly.LoadFrom(@"E:\hc\新建文件夹\ReflectionCode\插件机制\bin\Debug\Run.dll");

                    Type t3 = objAss3.GetType("Run.Run");

                    //动态生成类StringUtil的实例   
                    object obj3 = System.Activator.CreateInstance(t3);

                    //找到对应的方法   
                    MethodInfo p3 = t3.GetMethod("GetMsg");

                    object objRetval3 = p3.Invoke(obj3, null);
                    richTextBox1.Text += objRetval3.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}
