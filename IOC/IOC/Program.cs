using System;
using System.Configuration;
using System.Reflection;
using DAL;
using Unity;

namespace IOC
{
    public  class Program
    {
        static UnityContainer container = new UnityContainer();
        static void Main(string[] args)
        {
            //普通调用方式
            //GoodsOperation opera = new GoodsOperation(new SF(),new ByAir());
            //opera.SendGoods();


            //通过unity(微软的IOC库)
            container.RegisterType<IExpress, SF>();
            container.RegisterType<ILogistic, ByAir>();
            
            GoodsOperation op1 = container.Resolve<GoodsOperation>();
            op1.SendGoods();

            Console.ReadKey();
        }
    }

    #region 一般方法

    public class Express
    {
        public void SendGoods(ByCar car)
        {
            Console.WriteLine("这是" + car.Name);
        }
    }

    public class ByCar
    {
        public string Name { get { return "陆运"; } }
    }
    #endregion


}
