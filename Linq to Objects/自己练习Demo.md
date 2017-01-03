###Linq跟Lambda的join写法不是很清楚 所以做了个练习
```
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// 这里主要是联系linq跟lambda的多表join的写法
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<Class> classess = new List<Class>() { new Class { ClassName = "一班", ClassId = 1 }, new Class { ClassName = "二班", ClassId = 2 } };

            List<Student> students = new List<Student>() { 
                new Student(){StudentId=1,Name="张三",Age=18,Gender="男",ClassId=1},
                new Student(){StudentId=2,Name="李四",Age=18,Gender="女",ClassId=1},
                new Student(){StudentId=3,Name="王五",Age=20,Gender="女",ClassId=1},
                new Student(){StudentId=4,Name="赵六",Age=119,Gender="女",ClassId=2},
                new Student(){StudentId=5,Name="张三",Age=23,Gender="男",ClassId=2},
                new Student(){StudentId=6,Name="胜七",Age=22,Gender="男",ClassId=2}
            };

            List<Subject> subjects = new List<Subject>() { 
                new Subject(){StudentId=1,SubName="数学",Score=80},
                new Subject(){StudentId=1,SubName="语文",Score=90},
                new Subject(){StudentId=2,SubName="数学",Score=88},
                new Subject(){StudentId=2,SubName="语文",Score=85},
                new Subject(){StudentId=3,SubName="数学",Score=89},
                new Subject(){StudentId=4,SubName="语文",Score=93},
                new Subject(){StudentId=5,SubName="数学",Score=78},
                new Subject(){StudentId=5,SubName="语文",Score=94},
                new Subject(){StudentId=6,SubName="数学",Score=88},
                new Subject(){StudentId=6,SubName="语文",Score=89},
                new Subject(){StudentId=6,SubName="英语",Score=90}
            };

            //输出一班所有的学生

            //写法一  左链接 
            var query = from s in students
                        join c in classess on s.ClassId equals c.ClassId
                        where s.ClassId == 1
                        select new
                        {
                            s.Name
                        };
            Console.WriteLine("一班的学生有:{0}",query.Aggregate("",(c,i)=>c+" "+i.Name));

            //写法二 自动链接
            query = from s in students
                        from c in classess where s.ClassId==c.ClassId
                        where s.ClassId == 2
                        select new
                        {
                            s.Name
                        };
            Console.WriteLine("二班的学生有:{0}", query.Aggregate("", (c, i) => c + " " + i.Name));

            //lambda写法
            query = students.Join(classess, s => s.ClassId, c => c.ClassId, (s, c) => new { s, c }).
                Where(item => item.s.ClassId == 1).Select(item => new { item.s.Name});
            Console.WriteLine("一班的学生有:{0}", query.Aggregate("", (c, i) => c + " " + i.Name));

        }
    }


    /// <summary>
    /// 班级类
    /// </summary>
    class Class
    {
        public string ClassName { get; set; }
        public int ClassId { get; set; }

        public List<Student> GetAllStudentByClassId(List<Student> students, int classId)
        {
            return students.Where(item => item.ClassId == classId).ToList();
        }
    }


    /// <summary>
    /// 学生类
    /// </summary>
    class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int ClassId { get; set; }

        public List<Subject> GetAllSubjectByStudentId(List<Subject> subjects, int studentId)
        {
            return subjects.Where(item => item.StudentId == studentId).ToList();
        }
    }

    /// <summary>
    /// 科目类
    /// </summary>
    class Subject
    {
        public int StudentId { get; set; }
        public string SubName { get; set; }
        public int Score { get; set; }
    }
}

```


###Lambda 用 GroupBy分组求某一列的Sum
```
dt.AsEnumerable().GroupBy(r => r["ShopName"])
                        .Select(group => new { ShopName = group.Key, SumCount = group.Sum(item => Convert.ToInt32(item["SellCount"])) })
```
重点在于 **group.Sum(item => Convert.ToInt32(item["SellCount"]))**这一句
