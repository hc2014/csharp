using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq_Learning
{
    /// <summary>
    ///  定义一级方程式世界车队冠军信息
    /// </summary>
    public class Team
    {
        /// <summary>
        /// 车队名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获的车队冠军的年份
        /// </summary>
        public int[] Years { get; set; }
    }
}
