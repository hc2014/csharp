using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace 反射优化
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("固定赋值{0}", "-".PadRight(30, '-'));
            //test("123");
            //Console.WriteLine("GUID赋值{0}", "-".PadRight(30, '-'));
            //test(Guid.NewGuid().ToString());


            User u = new User();
            //PropertyInfo p = typeof(User).GetProperty("Age");
            //if (p.CanWrite)
            //{
            //    p.SetValue(u,"26");
            //}

            //设置属性的值
            //MethodInfo setAge = p.GetSetMethod(true);
            //setAge.Invoke(u, new object[] { "26" });

            //利用表达式树 设置属性的值
            //var property = typeof(User).GetProperty("Age");
            //var target = Expression.Parameter(typeof(User));
            //var propertyValue = Expression.Parameter(typeof(string),"age");
            //var setPropertyValue = Expression.Call(target, property.GetSetMethod(), propertyValue);
            //var setAction = Expression.Lambda<Action<User, string>>(setPropertyValue, target, propertyValue).Compile();
            //setAction(u, "26");



            //用Emit 实现上面同样的代码
            //var property = typeof(User).GetProperty("Age");
            //DynamicMethod method = new DynamicMethod("SetValue", null, new Type[] { typeof(User), typeof(string) });
            //ILGenerator ilGenerator = method.GetILGenerator();
            //ilGenerator.Emit(OpCodes.Ldarg_0);
            //ilGenerator.Emit(OpCodes.Ldarg_1);
            //ilGenerator.EmitCall(OpCodes.Callvirt, property.GetSetMethod(), null);
            //ilGenerator.Emit(OpCodes.Ret);

            //method.DefineParameter(1, ParameterAttributes.In, "obj");
            //method.DefineParameter(2, ParameterAttributes.In, "value");
            //var setAction = (Action<User, string>)method.CreateDelegate(typeof(Action<User, string>));
            //setAction(u, "26");




            //利用委托获取属性的值
            //u.Age="26";
            //Func<User, string> getAge = (Func<User, string>)Delegate.CreateDelegate(typeof(Func<User, string>), p.GetGetMethod());
            //string age = getAge(u);



            //用表达式树来创建委托的方式 获取属性值
            ////lambda的参数u 
            //var param_u = Expression.Parameter(typeof(User), "u");
            ////lambda的方法体 u.Age 
            //var pGetter = Expression.Property(param_u, p);
            ////编译lambda 
            //var getAge = Expression.Lambda<Func<User, string>>(pGetter, param_u).Compile();
            //u.Age = "26";
            //string age = getAge(u);


            //用Emit的方式实现 获取属性
            var getpProperty = typeof(User).GetProperty("Age");
            DynamicMethod getValueMethod = new DynamicMethod("GetValue", typeof(string), new Type[] { typeof(User) });

            ILGenerator ilGenerator = getValueMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.EmitCall(OpCodes.Callvirt, getpProperty.GetGetMethod(), null);
            ilGenerator.Emit(OpCodes.Ret);

            getValueMethod.DefineParameter(1, ParameterAttributes.In, "target");
            var getFunc = (Func<User, string>)getValueMethod.CreateDelegate(typeof(Func<User, string>));
            var age=getFunc(u);

            Console.ReadKey();
        }


        static void test(string val)
        {
            int count = 1000000;

            User testObj = new User();
            PropertyInfo propInfo = typeof(User).GetProperty("ID");

            Console.Write("直接访问花费时间：       ");
            Stopwatch watch1 = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
                testObj.ID = val;

            watch1.Stop();
            Console.WriteLine(watch1.Elapsed.ToString());

            Action<object, object> setter2 = DynamicMethodFactory.CreatePropertySetter(propInfo);
            Console.Write("EmitSet花费时间：        ");
            Stopwatch watch2 = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
                setter2(testObj, val);

            watch2.Stop();
            Console.WriteLine(watch2.Elapsed.ToString());

            Console.Write("纯反射花费时间：　       ");
            Stopwatch watch3 = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
                propInfo.SetValue(testObj, val, null);

            watch3.Stop();
            Console.WriteLine(watch3.Elapsed.ToString());


            Console.Write("利用委托花费时间：　       ");
            Stopwatch watch4 = Stopwatch.StartNew();


            MethodInfo setAge = propInfo.GetSetMethod();
            //利用委托获取属性的值
            for (int i = 0; i < count; i++)
            {
                setAge.Invoke(testObj, new object[] { val });
            }
            watch4.Stop();
            Console.WriteLine(watch4.Elapsed.ToString());
        }


        //对比序列化
        static void test2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Code");
            dt.Columns.Add("Age");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Email");
            dt.Columns.Add("QQ");
            dt.Columns.Add("Address");
            for (int i = 0; i < 1000; i++)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    dr[col] = col.ColumnName + i;
                }
                dt.Rows.Add(dr);
            }

            Stopwatch watch1 = Stopwatch.StartNew();

            var list = DataTableUtility.Get<User>(dt).ToList();
            watch1.Stop();
            Console.WriteLine("用表达式树:{0}", watch1.Elapsed.ToString());
            List<User> uList = new List<User>();


            Stopwatch watch2 = Stopwatch.StartNew();
            foreach (DataRow dr in dt.Rows)
            {
                User u = new User();
                Type t = u.GetType();
                var pList = t.GetProperties();
                var pNameList = pList.Select(r => r.Name).ToList();
                foreach (DataColumn col in dt.Columns)
                {
                    if (pNameList.Contains(col.ColumnName))
                    {
                        t.GetProperty(col.ColumnName).SetValue(u, dr[col.ColumnName]);
                    }
                }
                uList.Add(u);
            }
            watch2.Stop();
            Console.WriteLine("用反射:{0}", watch2.Elapsed.ToString());

        }

    }

    public static class DynamicMethodFactory
    {
        public static Action<object, object> CreatePropertySetter(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            if (!property.CanWrite)
                return null;

            MethodInfo setMethod = property.GetSetMethod(true);

            DynamicMethod dm = new DynamicMethod("PropertySetter", null,
                new Type[] { typeof(object), typeof(object) }, property.DeclaringType, true);

            ILGenerator il = dm.GetILGenerator();

            if (!setMethod.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
            }
            il.Emit(OpCodes.Ldarg_1);

            EmitCastToReference(il, property.PropertyType);
            if (!setMethod.IsStatic && !property.DeclaringType.IsValueType)
            {
                il.EmitCall(OpCodes.Callvirt, setMethod, null);
            }
            else
                il.EmitCall(OpCodes.Call, setMethod, null);

            il.Emit(OpCodes.Ret);   

            return (Action<object, object>)dm.CreateDelegate(typeof(Action<object, object>));
        }

        private static void EmitCastToReference(ILGenerator il, Type type)
        {
            if (type.IsValueType)
                il.Emit(OpCodes.Unbox_Any, type);
            else
                il.Emit(OpCodes.Castclass, type);
        }
    }

    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string QQ { get; set; }
    }

    
}
