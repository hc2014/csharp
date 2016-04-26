using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotNet.WinForm.Nyso.Trade.TradeListPrint
{
    public partial class CustomerComBoBox : ComboBox
    {
        public CustomerComBoBox()
        {
            InitializeComponent();
        }

        public CustomerComBoBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //导入API函数
        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);//返回hWnd参数所指定的窗口的设备环境。

        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC); //函数释放设备上下文环境（DC）

        protected override void WndProc(ref   Message m)
        {
            base.WndProc(ref   m);
            //WM_PAINT = 0xf; 要求一个窗口重画自己,即Paint事件时
            //WM_CTLCOLOREDIT = 0x133;当一个编辑型控件将要被绘制时发送此消息给它的父窗口；
            //通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
            //windows消息值表,可参考:http://hi.baidu.com/dooy/blog/item/0e770a24f70e3b2cd407421b.html
            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                IntPtr hDC = GetWindowDC(m.HWnd);
                if (hDC.ToInt32() == 0) //如果取设备上下文失败则返回
                {
                    return;
                }

                //建立Graphics对像
                Graphics g = Graphics.FromHdc(hDC);
                //画边框的 
                ControlPaint.DrawBorder(g, new Rectangle(0, 0, Width, Height), Color.Black, ButtonBorderStyle.Outset);
                //画坚线
                ControlPaint.DrawBorder(g, new Rectangle(Width - Height, 0, Height, Height), Color.Black, ButtonBorderStyle.Outset);
                //g.DrawLine(new Pen(Brushes.Blue, 2), new PointF(this.Width - this.Height, 0), new PointF(this.Width - this.Height, this.Height));
                //释放DC  
                ReleaseDC(m.HWnd, hDC);
            }
        }


    }
}
