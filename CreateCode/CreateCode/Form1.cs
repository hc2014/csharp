using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace CreateCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string key = "01234567";

        private void Create_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = EncryptString(this.textBox1.Text.Trim(), key);
        }

        public static string EncryptString(string input, string sKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(result);
            }
        }


        public static string DecryptString(string input, string sKey)
        {
            string[] sInput = input.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(result);
            }
        }


        //解密
        private void button1_Click(object sender, EventArgs e)
        {
            string str=DecryptString(textBox2.Text, key);
            string ads = GetMacAddress();
            if (str.Split('|')[0] != ads)
            {
                MessageBox.Show("该设备没有注册");
                return;
            }
            DateTime date=Convert.ToDateTime(str.Split('|')[1]);
            if ( date< DateTime.Now.Date)
            {
                MessageBox.Show("注册码过期了");
                return;
            }

            textBox3.Text = str; 
        }

        //MAC地址
        private void button2_Click(object sender, EventArgs e)
        {
          this.textBox4.Text= GetMacAddress();
        }

        public static string GetMacAddress()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {

            }
        }

        /// <summary>
        /// 获取CPU信息
        /// </summary>
        /// <returns></returns>
        public String GetCpuID()   
        {   
            try   
            {   
                ManagementClass mc = new ManagementClass("Win32_Processor");   
                ManagementObjectCollection moc = mc.GetInstances();   
  
                String strCpuID = null;   
                foreach (ManagementObject mo in moc)   
                {   
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();   
                    break;   
                }   
                return strCpuID;   
            }   
            catch   
            {   
                return "";   
            }   
        }


        /// <summary>
        /// 如果需要可以加入 cpu序列的认证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "60:57:18:89:F2:8D" + "|" + "2016-4-10";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox5.Text=GetCpuID();
        }
    }
}
