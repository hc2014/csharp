using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;

namespace Linq_Learning
{
    //public static class Enumerable
    //{
    //    public static IEnumerable<TSource> Where<TSource>(
    //        this IEnumerable<TSource> source, Func<TSource, bool> predicate);
    //}

    //public static class Queryable
    //{
    //    public static IQueryable<TSource> Where<TSource>(
    //        this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
    //}

    static class MyEnumerable
    {
        // 自定义扩展方法示例
        public static IEnumerable<TSource> MyWhere<TSource>(
            this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }

    class Program
    {
        static string Hello(string a, string b)
        {
            return "欢迎光临我的博客" + Environment.NewLine + a + " " + b;
        }

        static void Main(string[] args)
        {

            #region begion

            // 委托函数
            Func<string, string, string> func1 = Hello;
            // 匿名方法
            Func<string, string, string> func2 =
                delegate(string a, string b)
                {
                    return "欢迎光临我的博客" + Environment.NewLine + a + " " + b;
                };
            // Lambda表达式
            Func<string, string, string> func3 =
                (a, b) => { return "欢迎光临我的博客" + Environment.NewLine + a + " " + b; };

            // 调用Func委托
            string helloStr = func2("滴答的雨", @"http://www.cnblogs.com/heyuquan/");

            Console.WriteLine(helloStr);
            Console.WriteLine();

            #endregion

            #region 延迟计算注意点

            //DelayCalculate();

            #endregion

            #region 过滤操作符 （Where，OfType，Distinct）

            Where_Query();
            Where_Fluent();
            //OfType_Fluent();

            #endregion

            #region 投影操作符 （Select，SelectMany）

            SelectMany_Query();
            SelectMany_Fluent();

            #endregion

            #region 排序操作符 （Order，OrderByDescending，ThenBy，ThenByDescending，Reverse）

            //Order_Query();
            //Order_Fluent();
            //Reverse_Fluent();

            #endregion

            #region 连接操作符 （Join，GroupJoin）

            //Join_Query();
            //Join_Fluent();
            //GroupJoin_Query();
            //GroupJoin_Fluent();

            #endregion

            #region 分组操作符 （Group，GroupBy）

            //Group_Query();
            //Group_Fluent1();
            //Group_Fluent2();

            #endregion

            #region 量词操作符 （All、Any、Contains）

            //Any_Fluent();

            #endregion

            #region 分区操作符 （Skip,Take）

            //Paging_Query();

            #endregion

            #region 集合操作符 （Union，Concat，Intersect，Except，Zip，SequenceEqual）

            //Intersect_Fluent();
            //Zip_Fluent();
            //SequenceEqual_Fluent();

            #endregion

            #region 元素操作符 （First，FirstOrDefault，Last，LastOrDefault，ElementAt，ElementAtOrDefault，Single，SingleOrDefault）

            //ElementAt_Fluent();

            #endregion

            #region 合计操作符 （Count、LongCount、Sum、Max、Min、Average、Aggregate）

            //Sum_Fluent();
            //Aggregate_Fluent();

            #endregion

            #region 转换操作符 （Cast，ToArray，ToList，ToDictionary，ToLookup，AsEnumerable，DefaultIfEmpty）

            //Lookup_Fluent();
            //DefaultIfEmpty_Fluent();

            #endregion

            #region 生成操作符 （Range，Empty，Repeat）

            //Range_Fluent();

            #endregion

            Console.Read();
        }

        #region 延迟计算注意点

        private static void DelayCalculate()
        {
            IEnumerable<char> query = "Not what you might expect";
            foreach (char item in "aeiou")
                query = query.Where(c => c != item);
            // 只删除了'u'----Not what yo might expect  
            foreach (char c in query)
                Console.Write(c);

            // 因为item变量是循环外部声明的，同一个变量重复声明更新,
            // 所以每个lambda表达式获取的是同样的item.之后枚举查询时，
            // 所有的lambda表达式引用了这个变量的当前值,即'u'。           
            Console.WriteLine();
            // 为了解决这个问题，必须将循环变量赋值到一个在循环代码块内声明的变量

            //IEnumerable<char> query1 = "Not what you might expect";
            //foreach (char item in "aeiou")
            //{
            //    char temp = item;
            //    query1 = query1.Where(c => c != temp);
            //}
            //// Nt wht y mght xpct  
            //foreach (char c in query1)
            //    Console.Write(c);
        }

        #endregion

        #region 过滤操作符 （Where，OfType，Distinct）
        /// <summary>
        /// 查询获得车手冠军次数大于15次且是Austria国家的一级方程式赛手
        /// </summary>
        private static void Where_Query()
        {
            Console.WriteLine();
            Console.WriteLine("过滤操作符：where示例（查询表达式）");

            var racer = from r in Formula1.GetChampions()
                        where r.Wins > 15 && r.Country == "Austria"
                        select r;
            foreach (var item in racer)
            {
                Console.WriteLine("{0:A}", item);
            }
        }

        /// <summary>
        /// 查询获得车手冠军次数大于15次且是Austria国家的一级方程式赛手
        /// </summary>
        private static void Where_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("过滤操作符：where示例（方法语法）");

            var racer = Formula1.GetChampions().Where(r => r.Wins > 15
                && r.Country == "Austria");

            foreach (var item in racer)
            {
                Console.WriteLine("{0:A}", item);
            }
        }

        /// <summary>
        /// 过滤object数组中的元素，返回字符串类型的数组
        /// </summary>
        private static void OfType_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("过滤操作符：OfType()示例");

            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
        #endregion

        #region 投影操作符 （Select，SelectMany）

        /// <summary>
        /// （Racer类定义了一个属性Cars，Cars是一个字符串数组。）过滤驾驶Ferrari的所有冠军
        /// </summary>
        private static void SelectMany_Query()
        {
            Console.WriteLine();
            Console.WriteLine("投影操作符：SelectMany示例（查询表达式）");

            var ferrariDrivers = from r in Formula1.GetChampions()
                                 from c in r.Cars
                                 where c == "Ferrari"
                                 orderby r.LastName
                                 select r.FirstName + " " + r.LastName;
            foreach (var item in ferrariDrivers)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// （Racer类定义了一个属性Cars，Cars是一个字符串数组。）过滤驾驶Ferrari的所有冠军
        /// </summary>
        private static void SelectMany_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("投影操作符：SelectMany示例（方法语法）");
            var ferrariDrivers = Formula1.GetChampions()
                .SelectMany(
                    r => r.Cars,
                    (r, c) => new { Racer = r, Car = c }
                )
                .Where(r => r.Car == "Ferrari")
                .OrderBy(r => r.Racer.LastName)
                .Select(r => r.Racer.FirstName + " " + r.Racer.LastName);
            foreach (var item in ferrariDrivers)
            {
                Console.WriteLine(item);
            }
        }

        #endregion

        #region 排序操作符 （Order，OrderByDescending，ThenBy，ThenByDescending，Reverse）

        private static void Order_Query()
        {
            Console.WriteLine();
            Console.WriteLine("排序操作符：排序操作符 示例（查询表达式）");
            var racers = from r in Formula1.GetChampions()
                         orderby r.Country, r.LastName descending, r.FirstName
                         select r;
            foreach (var item in racers)
            {
                Console.WriteLine(item);
            }
        }

        private static void Order_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("排序操作符：排序操作符 ectMany示例（方法语法）");
            var racers = Formula1.GetChampions()
                .OrderBy(r => r.Country)
                .ThenByDescending(r => r.LastName)
                .ThenBy(r => r.FirstName);
            foreach (var item in racers)
            {
                Console.WriteLine(item);
            }
        }

        private static void Reverse_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("排序操作符：Reverse 示例（方法语法）");
            var racers = Formula1.GetChampions()
                .OrderBy(r => r.Country)
                .Reverse();
            foreach (var item in racers)
            {
                Console.WriteLine(item);
            }
        }

        #endregion

        #region 连接操作符 （Join，GroupJoin）

        /// <summary>
        /// 返回1958到1965年间的车手冠军和车队冠军信息，根据年份关联
        /// </summary>
        private static void Join_Query()
        {
            Console.WriteLine();
            Console.WriteLine("连接操作符：Join 示例（查询表达式）");
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         where y > 1958 && y < 1965
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetContructorChampions()
                        from y in t.Years
                        where y > 1958 && y < 1965
                        select new { Year = y, Name = t.Name };

            var racersAndTeams = from r in racers
                                 join t in teams on r.Year equals t.Year
                                 select new
                                 {
                                     Year = r.Year,
                                     Racer = r.Name,
                                     Team = t.Name
                                 };
            Console.WriteLine("年份\t\t车手冠军\t\t车队冠军");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}"
                    , item.Year, item.Racer, item.Team);
            }
        }

        /// <summary>
        /// 返回1958到1965年间的车手冠军和车队冠军信息，根据年份关联
        /// </summary>
        private static void Join_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("连接操作符：Join 示例（方法语法）");
            var racers = Formula1.GetChampions()
                .SelectMany(y => y.Years, (r, y)
                    => new { Year = y, Name = r.FirstName + " " + r.LastName })
                .Where(ty => ty.Year > 1958 && ty.Year < 1965);

            var teams = Formula1.GetContructorChampions()
                .SelectMany(y => y.Years, (t, y) => new { Year = y, Name = t.Name })
                .Where(ty => ty.Year > 1958 && ty.Year < 1965);

            var racersAndTeams = racers.Join(teams
                    , r => r.Year, t => t.Year
                    , (r, t) => new { Year = r.Year, Racer = r.Name, Team = t.Name }
                );

            Console.WriteLine("年份\t\t车手冠军\t\t车队冠军");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}"
                    , item.Year, item.Racer, item.Team);
            }
        }

        /// <summary>
        /// 返回1958到1965年间的车手冠军和车队冠军信息，根据年份关联并分组
        /// </summary>
        private static void GroupJoin_Query()
        {
            Console.WriteLine();
            Console.WriteLine("连接操作符：GroupJoin 示例（查询表达式）");
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         where y > 1958 && y < 1965
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetContructorChampions()
                        from y in t.Years
                        where y > 1958 && y < 1965
                        select new { Year = y, Name = t.Name };

            var racersAndTeams = from r in racers
                                 join t in teams on r.Year equals t.Year
                                 into groupTeams
                                 select new
                                 {
                                     Year = r.Year,
                                     Racer = r.Name,
                                     GroupTeams = groupTeams
                                 };
            Console.WriteLine("年份\t\t车手冠军\t\t车队冠军数");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}"
                    , item.Year, item.Racer, item.GroupTeams.Count());
            }
        }

        /// <summary>
        /// 返回1958到1965年间的车手冠军和车队冠军信息，根据年份关联并分组
        /// </summary>
        private static void GroupJoin_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("连接操作符：GroupJoin 示例（方法语法）");
            var racers = Formula1.GetChampions()
                .SelectMany(y => y.Years, (r, y)
                    => new { Year = y, Name = r.FirstName + " " + r.LastName })
                .Where(ty => ty.Year > 1958 && ty.Year < 1965);

            var teams = Formula1.GetContructorChampions()
                .SelectMany(y => y.Years, (t, y) => new { Year = y, Name = t.Name })
                .Where(ty => ty.Year > 1958 && ty.Year < 1965);

            var racersAndTeams = racers
                .GroupJoin(teams
                    , r => r.Year, t => t.Year
                    , (r, t) => new { Year = r.Year, Racer = r.Name, GroupTeams = t }
                );

            Console.WriteLine("年份\t\t车手冠军\t\t车队冠军数");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}"
                    , item.Year, item.Racer, item.GroupTeams.Count());
            }
        }

        #endregion

        #region 分组操作符 （Group，GroupBy）

        /// <summary>
        /// 按城市分组，获取每个城市的车手冠军
        /// </summary>
        private static void Group_Query()
        {
            Console.WriteLine();
            Console.WriteLine("分组操作符：Group 示例（查询表达式）");
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            select new { Country = g.Key, Racers = g };

            Console.WriteLine("城市\t\t车手冠军数");
            foreach (var item in countries)
            {
                Console.WriteLine("{0}\t\t{1}", item.Country, item.Racers.Count());
            }
        }

        /// <summary>
        /// 按城市分组，获取每个城市的车手冠军
        /// </summary>
        private static void Group_Fluent1()
        {
            Console.WriteLine();
            Console.WriteLine("分组操作符：Group 示例1（方法语法）");
            var countries = Formula1.GetChampions()
                .GroupBy(r => r.Country)
                .Select(g => new { Country = g.Key, Racer = g });

            Console.WriteLine("城市\t\t车手冠军数");
            foreach (var item in countries)
            {
                Console.WriteLine("{0}\t\t{1}", item.Country, item.Racer.Count());
            }
        }

        /// <summary>
        /// 按城市分组，获取每个城市的车手冠军
        /// </summary>
        private static void Group_Fluent2()
        {
            Console.WriteLine();
            Console.WriteLine("分组操作符：Group 示例2（方法语法）");
            var countries = Formula1.GetChampions()
                 .GroupBy(r => r.Country, (k, g) => new { Country = k, Racer = g });

            Console.WriteLine("城市\t\t车手冠军数");
            foreach (var item in countries)
            {
                Console.WriteLine("{0}\t\t{1}", item.Country, item.Racer.Count());
            }
        }

        #endregion

        #region 量词操作符 （All、Any、Contains）

        /// <summary>
        /// 获取是否存在姓为“Schumacher”的车手冠军
        /// </summary>
        private static void Any_Fluent()
        {
            Console.WriteLine();
            Console.WriteLine("量词操作符：All 示例（方法语法）");
            var hasRacer_Schumacher = Formula1.GetChampions()
                .Any(r => r.LastName == "Schumacher");
            Console.WriteLine("是否存在姓为“Schumacher”的车手冠军？{0}", hasRacer_Schumacher ? "是" : "否");
        }

        #endregion

        #region 分区操作符 （Skip,Take）

        // 扩展方法Take()和Skip()操作添加到查询的最后可用于分页

        /// <summary>
        /// 将车手冠军列表按每页5个名字进行分页
        /// </summary>
        private static void Paging_Query()
        {
            int pageSize = 5;

            int numberPages = (int)Math.Ceiling(
                Formula1.GetChampions().Count() / (double)pageSize);

            for (int page = 0; page < numberPages; page++)
            {
                Console.WriteLine("Page {0}", page);

                var racers = (
                              from r in Formula1.GetChampions()
                              orderby r.LastName
                              select r.FirstName + " " + r.LastName
                              )
                              .Skip(page * pageSize).Take(pageSize);

                foreach (var name in racers)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }

        #endregion

        #region 集合操作符 （Union，Concat，Intersect，Except，Zip，SequenceEqual）

        /// <summary>
        /// 获取使用车型”Ferrari”和车型”Mclaren”都获得过车手冠军车手列表
        /// </summary>
        private static void Intersect_Fluent()
        {
            Func<string, IEnumerable<Racer>> racersByCar =
                Car => from r in Formula1.GetChampions()
                       from c in r.Cars
                       where c == Car
                       orderby r.LastName
                       select r;

            foreach (var racer in racersByCar("Ferrari")
                .Intersect(racersByCar("McLaren")))
            {
                Console.WriteLine(racer);
            }
        }

        /// <summary>
        /// 合并html开始标签和结束标签
        /// </summary>
        private static void Zip_Fluent()
        {
            string[] start = { "<html>", "<head>", "<body>" };
            string[] end = { "</html>", "</head>", "</body>" };

            var tags = start.Zip(end, (s, e) => { return s + e; });

            foreach (string item in tags)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// 判断两个序列是否相等，需要内容及顺序都相等
        /// </summary>
        private static void SequenceEqual_Fluent()
        {
            int[] arr1 = { 1, 4, 7, 9 };
            int[] arr2 = { 1, 7, 9, 4 };
            Console.WriteLine("排序前 是否相等：{0}"
                , arr1.SequenceEqual(arr2) ? "是" : "否");  // 否
            Console.WriteLine();
            Console.WriteLine("排序后 是否相等：{0}"
                , arr1.SequenceEqual(arr2.OrderBy(k => k)) ? "是" : "否"); // 是
        }

        #endregion

        #region 元素操作符 （First，FirstOrDefault，Last，LastOrDefault，ElementAt，ElementAtOrDefault，Single，SingleOrDefault）

        /// <summary>
        /// 获取冠军数排名第三的车手冠军
        /// </summary>
        private static void ElementAt_Fluent()
        {
            var Racer3 = Formula1.GetChampions()
                .OrderByDescending(r => r.Wins)
                .ElementAtOrDefault(2);
            Console.WriteLine("获取冠军数排名第三的车手冠军为：{0} {1},获奖次数：{2}"
                , Racer3.LastName, Racer3.FirstName, Racer3.Wins);
        }

        #endregion

        #region 合计操作符 （Count、LongCount、Sum、Max、Min、Average、Aggregate）

        private static void Sum_Fluent()
        {
            var racerCount = Formula1.GetChampions().Count();
        }

        private static void Aggregate_Fluent()
        {
            int[] numbers = { 1, 2, 3 };
            // 1+2+3 = 6
            int y = numbers.Aggregate((prod, n) => prod + n);
            // 0+1+2+3 = 6
            int x = numbers.Aggregate(0, (prod, n) => prod + n);
            // （0+1+2+3）*2 = 12
            int z = numbers.Aggregate(0, (prod, n) => prod + n, r => r * 2);
        }

        #endregion

        #region 转换操作符 （Cast，ToArray，ToList，ToDictionary，ToLookup，AsEnumerable，DefaultIfEmpty）

        /// <summary>
        /// 将车手冠军按其使用车型进行分组，并显示使用”williams”车型的车手名字。
        /// </summary>
        private static void Lookup_Fluent()
        {
            ILookup<string, Racer> racers =
                (from r in Formula1.GetChampions()
                 from c in r.Cars
                 select new
                 {
                     Car = c,
                     Racer = r
                 }
                 ).ToLookup(cr => cr.Car, cr => cr.Racer);

            if (racers.Contains("Williams"))
            {
                foreach (var williamsRacer in racers["Williams"])
                {
                    Console.WriteLine(williamsRacer);
                }
            }
        }

        private static void DefaultIfEmpty_Fluent()
        {
            var defaultArrCount = (new int[0]).DefaultIfEmpty().Count(); // 1
            Console.WriteLine("空int数组的DefaultIfEmpty返回的集合元素个数为:{0}", defaultArrCount);
        }

        #endregion

        #region 生成操作符 （Range，Empty，Repeat）

        // 生成操作符 Range()，Empty()，Repeat() 不是扩展方法，而是返回序列的正常静态方法。

        private static void Range_Fluent()
        {
            var values = Enumerable.Range(1, 20);
            foreach (var item in values)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
        }

        #endregion

    }
}
