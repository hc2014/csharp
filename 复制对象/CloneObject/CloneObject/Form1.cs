using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Reflection;

namespace CloneObject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ItemObjce item = new ItemObjce();
            item.Date = new DateTime(2021,1,1);
            item.ID = 1;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("字符串", "aaaaa");
            dic.Add("数字", 10.2m);
            dic.Add("对象", new DicObject {  Values="哈哈"});
            item.Dic = dic;

            BaseModel model = new BaseModel {  Age=13, Item=item, Name="测试"};

            //浅拷贝
            BaseModel newModel = model.Clone();

            //修改原对象数据  浅拷贝的对象newModel的数据也一样改变了
            model.Item.Date = DateTime.Now;
            model.Item.Dic.Add("新增对象","哈哈");

            //深拷贝
            BaseModel new2Model = DeepCopyByReflection<BaseModel>(model);

            //修改数据 新对象的数据不会改变
            model.Age = 10;
            model.Item.Dic.Remove("新增对象");
        }

        public static T DeepCopyByReflection<T>(T obj)
        {
            if (obj is string || obj.GetType().IsValueType)
                return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopyByReflection(field.GetValue(obj)));
                }
                catch { }
            }

            return (T)retval;
        }
    }

 


    public class BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public ItemObjce Item;
        public BaseModel Clone()
        {
            return (BaseModel)this.MemberwiseClone();
        }

       
    }

    public class ItemObjce
    {
        public DateTime Date { get; set; }
        public Dictionary<string, object> Dic { get; set; }

        public int ID { get; set; }
    }

    public class DicObject
    {
        public string Values { get; set; }
    }

}
