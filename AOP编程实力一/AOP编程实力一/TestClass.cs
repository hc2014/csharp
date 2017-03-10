using KingAOP;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP编程实力一
{
    class TestClass : IDynamicMetaObjectProvider
    {
        [LogAspec]
        public string TestMethodReturn()
        {
            return "测试方法返回值";
        }

        [LogAspec]
        public void TestMethodException()
        {
            throw new Exception("测试异常");
        }

        [LogAspec]
        public string TestMethodArguments(string  str,int i)
        {
            return str + i;
        }
        DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression parameter)
        {
            return new AspectWeaver(parameter, this);
        }
    }
}
