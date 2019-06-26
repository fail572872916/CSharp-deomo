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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.text_prot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_link
            // 
            this.bt_link.Location = new System.Drawing.Point(215, 18);
            this.bt_link.Name = "bt_link";
            this.bt_link.Size = new System.Drawing.Size(75, 23);
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
            this.listBox1_links.Location = new System.Drawing.Point(22, 65);
            this.listBox1_links.Name = "listBox1_links";
            this.listBox1_links.Size = new System.Drawing.Size(157, 460);
            this.listBox1_links.TabIndex = 2;
            this.listBox1_links.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox1_links_DrawItem);
            this.listBox1_links.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.ListBox1_links_MeasureItem);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(215, 326);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(544, 199);
            this.textBox2.TabIndex = 0;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(215, 74);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(544, 176);
            this.textBox3.TabIndex = 3;
            // 
            // text_prot
            // 
            this.text_prot.Location = new System.Drawing.Point(79, 18);
            this.text_prot.Name = "text_prot";
            this.text_prot.Size = new System.Drawing.Size(100, 21);
            this.text_prot.TabIndex = 4;
            this.text_prot.Text = "1234";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "端口号:";
            // 
            // bt_send
            // 
            this.bt_send.Location = new System.Drawing.Point(789, 478);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(86, 47);
            this.bt_send.TabIndex = 6;
            this.bt_send.Text = "发送";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.Bt_send_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 565);
            this.Controls.Add(this.bt_send);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_prot);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.listBox1_links);
            this.Controls.Add(this.bt_link);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_link;
        private System.Windows.Forms.ListBox listBox1_links;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox text_prot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_send;
    }
}

