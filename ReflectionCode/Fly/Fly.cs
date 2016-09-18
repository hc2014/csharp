using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fly
{
    public class Fly
    {
        string msg = string.Empty;
        string currentnamespace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }
        public string GetMsg()
        {
            return string.Format("{2}:选择了{0},所以就{1}\r\n", msg, currentnamespace,DateTime.Now.ToString());
        }
    }
}
