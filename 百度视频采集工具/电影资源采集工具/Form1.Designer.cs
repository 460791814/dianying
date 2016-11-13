namespace 电影资源采集工具
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStrat = new System.Windows.Forms.Button();
            this.listLog = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtbdMovieId = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.CB_Year = new System.Windows.Forms.ComboBox();
            this.CB_Area = new System.Windows.Forms.ComboBox();
            this.CB_Type = new System.Windows.Forms.ComboBox();
            this.Cb_Channel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndPage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnTV = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStrat
            // 
            this.btnStrat.Location = new System.Drawing.Point(15, 20);
            this.btnStrat.Name = "btnStrat";
            this.btnStrat.Size = new System.Drawing.Size(122, 23);
            this.btnStrat.TabIndex = 0;
            this.btnStrat.Text = "采集百度电影";
            this.btnStrat.UseVisualStyleBackColor = true;
            this.btnStrat.Click += new System.EventHandler(this.btnStrat_Click);
            // 
            // listLog
            // 
            this.listLog.FormattingEnabled = true;
            this.listLog.ItemHeight = 12;
            this.listLog.Location = new System.Drawing.Point(6, 20);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(553, 172);
            this.listLog.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.txtbdMovieId);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.CB_Year);
            this.groupBox1.Controls.Add(this.CB_Area);
            this.groupBox1.Controls.Add(this.CB_Type);
            this.groupBox1.Controls.Add(this.Cb_Channel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndPage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.txtPage);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 202);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工具栏";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "电影或电视剧ID:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(309, 134);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "根据ID进行采集";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtbdMovieId
            // 
            this.txtbdMovieId.Location = new System.Drawing.Point(137, 136);
            this.txtbdMovieId.Name = "txtbdMovieId";
            this.txtbdMovieId.Size = new System.Drawing.Size(152, 21);
            this.txtbdMovieId.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(205, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "采集";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CB_Year
            // 
            this.CB_Year.FormattingEnabled = true;
            this.CB_Year.Location = new System.Drawing.Point(407, 20);
            this.CB_Year.Name = "CB_Year";
            this.CB_Year.Size = new System.Drawing.Size(106, 20);
            this.CB_Year.TabIndex = 13;
            // 
            // CB_Area
            // 
            this.CB_Area.FormattingEnabled = true;
            this.CB_Area.Location = new System.Drawing.Point(293, 20);
            this.CB_Area.Name = "CB_Area";
            this.CB_Area.Size = new System.Drawing.Size(91, 20);
            this.CB_Area.TabIndex = 12;
            // 
            // CB_Type
            // 
            this.CB_Type.FormattingEnabled = true;
            this.CB_Type.Location = new System.Drawing.Point(170, 20);
            this.CB_Type.Name = "CB_Type";
            this.CB_Type.Size = new System.Drawing.Size(94, 20);
            this.CB_Type.TabIndex = 11;
            // 
            // Cb_Channel
            // 
            this.Cb_Channel.FormattingEnabled = true;
            this.Cb_Channel.Location = new System.Drawing.Point(52, 20);
            this.Cb_Channel.Name = "Cb_Channel";
            this.Cb_Channel.Size = new System.Drawing.Size(91, 20);
            this.Cb_Channel.TabIndex = 10;
            this.Cb_Channel.SelectedIndexChanged += new System.EventHandler(this.Cb_Channel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "起始页：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "采集页数：";
            // 
            // txtEndPage
            // 
            this.txtEndPage.Location = new System.Drawing.Point(239, 58);
            this.txtEndPage.Name = "txtEndPage";
            this.txtEndPage.Size = new System.Drawing.Size(70, 21);
            this.txtEndPage.TabIndex = 4;
            this.txtEndPage.Text = "112";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "重启时间";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(412, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "4800";
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(65, 58);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(78, 21);
            this.txtPage.TabIndex = 1;
            this.txtPage.Text = "1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(431, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "完善图片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(303, 20);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(97, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "更新电影资源";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnTV
            // 
            this.btnTV.Location = new System.Drawing.Point(152, 20);
            this.btnTV.Name = "btnTV";
            this.btnTV.Size = new System.Drawing.Size(131, 23);
            this.btnTV.TabIndex = 6;
            this.btnTV.Text = "采集最新百度电视剧";
            this.btnTV.UseVisualStyleBackColor = true;
            this.btnTV.Click += new System.EventHandler(this.btnTV_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listLog);
            this.groupBox2.Location = new System.Drawing.Point(5, 326);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 195);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "处理日记";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStrat);
            this.groupBox3.Controls.Add(this.btnTV);
            this.groupBox3.Controls.Add(this.btnUpdate);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(11, 211);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(559, 109);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "快捷菜单";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 533);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "百度电影资源采集工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStrat;
        private System.Windows.Forms.ListBox listLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEndPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTV;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox CB_Year;
        private System.Windows.Forms.ComboBox CB_Area;
        private System.Windows.Forms.ComboBox CB_Type;
        private System.Windows.Forms.ComboBox Cb_Channel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtbdMovieId;
        private System.Windows.Forms.Label label4;
    }
}

