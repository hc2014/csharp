using System;
using AOPClassLibrary;

namespace AOP编程实例二
{
    public  class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var oUserTest1 = new User() { Name = "张三", PassWord = "123" };
                var oUserTest2 = new User() { Name = "李四", PassWord = "111" };

                var order1 = new Order() { Price = 1, Order_ID = "123" };
                var order = OrderOperation.GetInstance();
                //order.Order_ID = "2222";

                try
                {
                    order.GetPrice(order1);
                }
                catch (Exception)
                {

                    throw;
                }

                //var oUser = UserOperation.GetInstance();
                //oUser.Test(oUserTest1);
                //oUser.Test2(oUserTest1, oUserTest2);
                //oUser.Test3(oUserTest1);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

    }

  
    
}
