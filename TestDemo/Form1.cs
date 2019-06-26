using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.window.server.Server;
using TestDemo.Common;
using static test.window.server.Server.Push;

namespace TestDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Push push1;

        delegate void SetTextCallback(bool isShow);
        private void SetText(bool isShow)
        {
            bt_link.Enabled = isShow;
        }


        //delegate void a(int a,int b,int c ,int d, void);//定义一个委托111111111111111111

        private void Button1_Click(object sender, EventArgs e)
        {
            String port = text_prot.Text.ToString();


            ThreadPool.QueueUserWorkItem(new WaitCallback((object o) =>
            {
                push1 = new Push(500, 1024, 0, Convert.ToInt32(port));
                push1.Alarm += new Push.AlarmEventHandler(HostHandleAlarm);//将A类的方法与B类的事件 用 B类的委托绑定
                push1.Receive += new Push.ReceivemEventHandler(HostHandleAlarm);//将A类的方法与B类的事件 用 B类的委托绑定
                bt_link.Invoke(new SetTextCallback(SetText), false);


            }));

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //４.编写事件处理程序
        void HostHandleAlarm(object sender, bool isLink, EventArgs e)
        {

            string link = "已连接";
            if (!isLink) {
                link = "未连接";
            }


           

            Console.WriteLine($"Push{sender}"+ link);
            Console.WriteLine("主人: 抓住了小偷！" + sender);
            foreach (var item in push1.GetLink())
            {

                Console.WriteLine("列表" + JsonHelper.SerializeObject(item.Key)+ "_____" + JsonHelper.SerializeObject(item.Value.ToString()));
             
            }
          
       
        }



        //４.编写事件处理程序
        void HostHandleAlarm(object sender, byte[] b, EventArgs e)
        {
            Console.WriteLine($"Push已接收:{sender} 长度:{b.Length}");
        }
    }




}
