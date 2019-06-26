namespace TestDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_link = new System.Windows.Forms.Button();
            this.listBox1_links = new System.Windows.Forms.ListBox();
            this.textBox_send = new System.Windows.Forms.TextBox();
            this.textBox_receive = new System.Windows.Forms.TextBox();
            this.text_prot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_send = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_link
            // 
            this.bt_link.Location = new System.Drawing.Point(237, 24);
            this.bt_link.Name = "bt_link";
            this.bt_link.Size = new System.Drawing.Size(87, 37);
            this.bt_link.TabIndex = 0;
            this.bt_link.Text = "button1";
            this.bt_link.UseVisualStyleBackColor = true;
            this.bt_link.Click += new System.EventHandler(this.Button1_Click);
            // 
            // listBox1_links
            // 
            this.listBox1_links.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox1_links.FormattingEnabled = true;
            this.listBox1_links.ItemHeight = 20;
            this.listBox1_links.Location = new System.Drawing.Point(14, 97);
            this.listBox1_links.Name = "listBox1_links";
            this.listBox1_links.Size = new System.Drawing.Size(137, 540);
            this.listBox1_links.TabIndex = 2;
            this.listBox1_links.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox1_links_DrawItem);
            this.listBox1_links.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.ListBox1_links_MeasureItem);
            // 
            // textBox_send
            // 
            this.textBox_send.Location = new System.Drawing.Point(228, 546);
            this.textBox_send.Multiline = true;
            this.textBox_send.Name = "textBox_send";
            this.textBox_send.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_send.Size = new System.Drawing.Size(583, 81);
            this.textBox_send.TabIndex = 0;
            // 
            // textBox_receive
            // 
            this.textBox_receive.Location = new System.Drawing.Point(237, 97);
            this.textBox_receive.Multiline = true;
            this.textBox_receive.Name = "textBox_receive";
            this.textBox_receive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_receive.Size = new System.Drawing.Size(673, 377);
            this.textBox_receive.TabIndex = 3;
            this.textBox_receive.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_receive_MouseClick);
            // 
            // text_prot
            // 
            this.text_prot.Location = new System.Drawing.Point(65, 33);
            this.text_prot.Name = "text_prot";
            this.text_prot.Size = new System.Drawing.Size(86, 21);
            this.text_prot.TabIndex = 4;
            this.text_prot.Text = "1234";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "端口号:";
            // 
            // bt_send
            // 
            this.bt_send.Location = new System.Drawing.Point(824, 580);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(86, 47);
            this.bt_send.TabIndex = 6;
            this.bt_send.Text = "发送";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.Bt_send_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(171, 546);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "发送";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Turquoise;
            this.label3.Location = new System.Drawing.Point(171, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "接收";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 667);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_send);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_prot);
            this.Controls.Add(this.textBox_receive);
            this.Controls.Add(this.textBox_send);
            this.Controls.Add(this.listBox1_links);
            this.Controls.Add(this.bt_link);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_link;
        private System.Windows.Forms.ListBox listBox1_links;
        private System.Windows.Forms.TextBox textBox_send;
        private System.Windows.Forms.TextBox textBox_receive;
        private System.Windows.Forms.TextBox text_prot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

