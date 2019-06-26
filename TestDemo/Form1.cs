using System;
using System.Collections.Concurrent;
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

        delegate void SetListCallback(object obj);

        private void SetList(object obj)
        {
            listBox1_links.Items.Clear();


            foreach (var item in obj as ConcurrentDictionary<int, String>)
            {
                listBox1_links.Items.Add(item.Value);
            }

        }

        delegate void SetReceiveCallback(object obj);
        private void UpdateReceive(object obj) => textBox_receive.AppendText(obj as string);


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
            if (!isLink)
            {
                link = "未连接";
            }




            Console.WriteLine($"Push{sender}" + link);
            Console.WriteLine("主人: 抓住了小偷！" + sender);
            foreach (var item in push1.GetLink())
            {

                Console.WriteLine("列表" + JsonHelper.SerializeObject(item.Key) + "_____" + JsonHelper.SerializeObject(item.Value.ToString()));

            }


            listBox1_links.Invoke(new SetListCallback(SetList), push1.GetLink());

        }



        //４.编写事件处理程序
        void HostHandleAlarm(object sender, byte[] b, EventArgs e)
        {
            Console.WriteLine($"Push已接收:{sender} 长度:{b.Length}");


            foreach (var item in push1.GetLink())
            {

                if (item.Key == (int)sender) {

                    textBox_receive.Invoke(new SetReceiveCallback(UpdateReceive), item.Value + ":\t" + System.Text.Encoding.Default.GetString(b)+"\n");
                }

            }
        }






        /// <summary>
        /// 设置listbox的高度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_links_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index == -1)
            {
                return;
            }
            e.DrawBackground(); //绘制各项的背景色
            e.DrawFocusRectangle();
            //让文字位于Item的中间
            float difH = (e.Bounds.Height - e.Font.Height) / 2;
            //指定绘制文本的位置
            RectangleF rf = new RectangleF(e.Bounds.X, e.Bounds.Y + difH, e.Bounds.Width, e.Font.Height);
            //绘制指定的字符串
            e.Graphics.DrawString(listBox1_links.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), rf);


        }




        /// <summary>
        /// 设置listbox的高度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_links_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 20;

        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_send_Click(object sender, EventArgs e)
        {

            string send = textBox_send.Text.Trim().ToString();
            if (!string.IsNullOrEmpty(send))
            {

                byte[] arg2 = Encoding.UTF8.GetBytes(send);

                int id = GetSelectId(listBox1_links.SelectedItem.ToString());
                if (id != -1)
                {
                    push1.server.Send(id, arg2, 0, arg2.Length);
                }
            }
        }

        /// <summary>
        /// 根据选中的
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private int GetSelectId(String ipAddress)
        {


            foreach (var item in push1.GetLink())
            {

                if (ipAddress.Equals(item.Value))
                {

                    return item.Key;
                }

            }

            return -1;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           



        }

        /// <summary>
        /// 关闭窗体钱关闭所有连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //if (push1 != null)
            //{
            //    foreach (var item in push1.GetLink())
            //    {

            //        push1.server.Close(item.Key);

            //    }
            //}
            //Thread.Sleep(100);
        }

        private void TextBox_receive_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_receive.Text = "";
        }
    }

    



}
