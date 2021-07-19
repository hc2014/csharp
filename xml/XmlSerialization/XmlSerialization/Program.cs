using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            ClinicalDocument model = new ClinicalDocument();
            model.realmCode = new RealmCode { code = "CN" };
            model.TypeId = new TypeId { extension = "2.0.0.0.1", root = "POCD_MT000040" };
            model.docid = new TypeId { root = "111111", extension = "" };
            model.templateId = new TemplateId { root = "1.2.3.4.5.6" };
            model.Code = new Code { code = "HSDC00.01", codeSystem = "3333333", codeSystemName = "阿里巴巴和四十大盗" };
            model.title = "测试";

            model.MyDocument = new XmlDocument().CreateComment("第一个注释");

            List<TypeId> list = new List<TypeId>();
            list.Add(new TypeId { extension = "", root = "11111111111" });
            list.Add(new TypeId { extension = "", root = "2222222222" });
            list.Add(new TypeId { extension = "", root = "3333333333" });

            model.PatientRole = new PatientRole { classCode = "PAT", patientRole = list };
            model.time = new Time { value = "" };

            List<Base> testList = new List<Base>();
            testList.Add(new Base { value2 = "123", value4 = "444" });
            testList.Add(new Base { value3 = "123", value5 = "555555" });
            model.PatientRole.listObject = testList;


            List<ListItem> listItem = new List<ListItem>();
            listItem.Add(new ListItem { ID = "1", Name = "张三" });
            listItem.Add(new ListItem { ID = "2", Name = "李四" });

            model.ListItem = listItem;

            string xml = XmlSerialize<ClinicalDocument>(model);
            Console.WriteLine(xml);
            Console.ReadKey();
        }

        public static string XmlSerialize<T>(T obj)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    Type t = obj.GetType();
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(sw, obj);
                    sw.Close();
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }
    }


    public class ClinicalDocument
    {

        [XmlAnyElement("MyDocument")]
        public XmlComment MyDocument { get; set; }

        [XmlElement("realmCode")]
        public RealmCode realmCode { get; set; }
        [XmlElement("typeId")]
        public TypeId TypeId { get; set; }

        [XmlElement("templateId")]
        public TemplateId templateId { get; set; }

        [XmlElement("id")]
        public TypeId docid { get; set; }

        [XmlElement("code")]
        public Code Code { get; set; }

        [XmlElement]
        public string title { get; set; }

        public PatientRole PatientRole { get; set; }

        [XmlElement("time")]
        public Time time { get; set; }

        [XmlElement("listitem")]
        public List<ListItem> ListItem { get; set; }

    }

    public class ListItem
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }


    public class RealmCode
    {
        [XmlAttribute]
        public string code { get; set; }
    }

    public class TypeId
    {

        [XmlAttribute]
        public string root { get; set; }
        [XmlAttribute]
        public string extension { get; set; }
    }
    public class TemplateId
    {
        [XmlAttribute]
        public string root { get; set; }
    }

    public class Code
    {
        [XmlAttribute]
        public string code { get; set; }
        [XmlAttribute]
        public string codeSystem { get; set; }
        [XmlAttribute]
        public string codeSystemName { get; set; }
    }

    public class PatientRole
    {
        [XmlAttribute]
        public string classCode { get; set; }
        [XmlElement("id")]
        public List<TypeId> patientRole { get; set; }

        [XmlElement("baseItem")]
        public List<Base> listObject { get; set; }
    }



    public class Time
    {
        [XmlAttribute]
        public string value { get; set; }
        [XmlAttribute(Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string type = "TS";
    }


    public class Base
    {
        public string value1 { get; set; }
        public string value2 { get; set; }
        public string value3 { get; set; }
        public string value4 { get; set; }
        public string value5 { get; set; }
    }

    [Serializable]
    public class Sub1 : Base
    {
        public new string value2 { get; set; }
    }
    [Serializable]
    public class Sub2 : Base
    {
        public new string value3 { get; set; }
    }


    public class Root
    {
        public Node Node { get; set; }
    }

    public class Node
    {
        [XmlElement(typeof(LabelElement))]
        [XmlElement(typeof(TextBoxElement))]
        [XmlElement(typeof(ButtonElement))]
        public List<BaseElement> Elements { get; set; }
    }

    public abstract class BaseElement
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    public class ButtonElement : BaseElement
    {
        [XmlAttribute(AttributeName = "Label")]
        public string Label { get; set; }

        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }
    }

    public class LabelElement : BaseElement
    {
        [XmlText]
        public string Content { get; set; }
    }

    public class TextBoxElement : BaseElement
    {
        [XmlAttribute(AttributeName = "MultiLines")]
        public int MultiLines { get; set; }
    }
}
