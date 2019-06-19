using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Func<object, object> funcOne = delegate (object s)
        {
            return s;
        };
        private void Button1_Click(object sender, EventArgs e)
        {
            String port = text_prot.Text.ToString();

            

            Console.WriteLine(funcOne.ToString());


        }
    }
}
