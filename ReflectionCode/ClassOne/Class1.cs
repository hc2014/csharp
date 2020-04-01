using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassOne
{
    class StringUtil
    {
        public double GetSum(double x, double y)
        {
            return x + y;
        }

        public enum Color
        {
            Red=1,
            Yellow=2,
            Blue=3
        }

        enum State
        {
            Run=1,
            Stop=2,
            Reset=3
        }
    }

    class ChangeValue
    {
        // 这是私有字段
        private string myValue;

        public ChangeValue()
        {
        }

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
