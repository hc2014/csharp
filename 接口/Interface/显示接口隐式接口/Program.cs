using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 显示接口隐式接口
{
    class Program
    {
        static void Main(string[] args)
        {
            myImplicitClass  iClass = new myImplicitClass();
            iClass.myMethon();

            myExplicitClass eClass = new myExplicitClass();
            //类型转换成接口
            (eClass as myInterface).myMethon();
        }
    }


    interface myInterface
    {
        void myMethon();
    }

    public class myImplicitClass : myInterface
    {
        /// <summary>
        /// 传统模式就是这种隐式模式来实现接口方法的
        /// </summary>
        public void myMethon()
        {
            Console.WriteLine("传统的隐式实现接口方法");
        }
    }

    public class myExplicitClass : myInterface
    {
        /// <summary>
        /// 必须显示实现接口  接口名称.方法名  的方式来调用方法
        /// </summary>
        void myInterface.myMethon()
        {
            Console.WriteLine("显示实现接口方法");
        }
    }

}
