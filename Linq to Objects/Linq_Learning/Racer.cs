using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Linq_Learning
{
    /// <summary>
    /// 定义一级方程式世界车手冠军信息
    /// </summary>
    [Serializable]
    public class Racer : IComparable<Racer>, IFormattable
    {
        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 获的车手冠军次数
        /// </summary>
        public int Wins { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Starts { get; set; }
        /// <summary>
        /// 获得车手冠军时的车型列表
        /// </summary>
        public string[] Cars { get; set; }
        /// <summary>
        /// 获的车手冠军的年份
        /// </summary>
        public int[] Years { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }
        public int CompareTo(Racer other)
        {
            return this.LastName.CompareTo(other.LastName);
        }
        public string ToString(string format)
        {
            return ToString(format, null);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "C":
                    return Country;
                case "S":
                    return Starts.ToString();
                case "W":
                    return Wins.ToString();
                case "A":
                    return String.Format("{0} {1}, {2}; starts: {3}, wins: {4}",
                       FirstName, LastName, Country, Starts, Wins);
                default:
                    throw new FormatException(String.Format(
                       "Format {0} not supported", format));
            }
        }
    }
}