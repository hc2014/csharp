using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    /// <summary>
    /// 发货中心
    /// </summary>
    public class GoodsOperation
    {
        IExpress _express;
        ILogistic _logistic;
        public  GoodsOperation(IExpress exp, ILogistic logis)
        {
            _express = exp;
            _logistic = logis;
        }

        /// <summary>
        /// 发货
        /// </summary>
        public  void SendGoods()
        {
            _express.SendGoods(_logistic);
        }
    }


    public interface IExpress
    {
        void SendGoods(ILogistic logistic);
    }

    public interface ILogistic
    {
        string Name { get; }
    }


    public class SF : IExpress
    {
        public void SendGoods(ILogistic logistic)
        {
            Console.WriteLine(logistic.Name);
        }
    }

    public class ByAir : ILogistic
    {
        public string Name
        {
            get
            {
                return "空运";
            }
        }
    }

    public class ByCar : ILogistic
    {
        public string Name { get { return "陆运"; } }
    }
}
