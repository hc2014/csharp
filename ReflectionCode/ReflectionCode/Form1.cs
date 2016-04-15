using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ReflectionCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //参考的文章路径
            //http://www.cnblogs.com/kingcat/archive/2007/09/03/879873.html
        }

        private void btn_Click(object sender, EventArgs e)
        {
            //dll的路径
            Assembly objAss = Assembly.LoadFrom(@"E:\hc\新建文件夹\ReflectionCode\ClassOne\bin\Debug\ClassOne.dll");

            Type t = objAss.GetType("ClassOne.StringUtil");

            //动态生成类StringUtil的实例   
            object obj = System.Activator.CreateInstance(t);

            //参数信息，GetSum需要两个int参数   
            System.Type[] paramTypes = new System.Type[2];
            paramTypes[0] = System.Type.GetType("System.Int32");
            paramTypes[1] = System.Type.GetType("System.Int32");

            //找到对应的方法   
            MethodInfo p = t.GetMethod("GetSum", paramTypes);


            //参数值，如果所调用的方法没有参数，不用写这些   
            Object[] parameters = new Object[2];
            parameters[0] = 3;
            parameters[1] = 4;

            //如果没有参数，写null即可。 
            object objRetval = p.Invoke(obj, parameters);
            label1.Text = objRetval.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //这种是同一个项目中不同类之间的操作

            // 正常访问
            ChangeValue cv = new ChangeValue("old value");
            cv.WriteLine();

            //反射的方法直接访问私有字段
            Type t = cv.GetType();

            //得到私有字段对象并赋值
            FieldInfo field = t.GetField("myValue", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(cv, "new value");

            //输出的是新值 "new value"
            label1.Text = cv.WriteLine();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dll的路径
            Assembly objAss = Assembly.LoadFrom(@"E:\hc\新建文件夹\ReflectionCode\ClassOne\bin\Debug\ClassOne.dll");

            Type t = objAss.GetType("ClassOne.ChangeValue");

            //动态生成类StringUtil的实例   
            object obj = System.Activator.CreateInstance(t);

            FieldInfo field = t.GetField("myValue", BindingFlags.NonPublic | BindingFlags.Instance);

            field.SetValue(obj, "new value");

            MethodInfo p = t.GetMethod("WriteLine");

            object objRetval = p.Invoke(obj,null);
            label1.Text = objRetval.ToString();
        }
    }

    class ChangeValue
    {
        // 这是私有字段
        private string myValue;

        // 一般只有通过这样的公共属性外面才可能访问私有字段
        public ChangeValue(string str)
        {
            myValue = str;
        }
        public string WriteLine()
        {
            return "MyValue is: " + myValue;
        }
    }
}
