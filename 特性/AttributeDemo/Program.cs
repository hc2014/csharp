using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AttributeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //ObsoleteMethon();
            NewMethon();
        }

        [Obsolete("这个方法已经过时了,请使用NewMethon方法")]
        static void ObsoleteMethon()
        {
            Console.WriteLine("过时的方法");
        }

        [CustomAttribute("张三", "2016-12-30", Msg = "测试属性")]
        static void NewMethon()
        {
            Console.WriteLine("新方法");
        }
    }

    /// <summary>
    /// 这个是自定义的特性
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    [ComVisible(true)]
    public class CustomAttribute : Attribute
    {

        public CustomAttribute(string name, string datetime)
        {
            DateTime = Convert.ToDateTime(datetime);
            Name = name;

        }
        public string Msg { get; set; }
        public string Name { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
