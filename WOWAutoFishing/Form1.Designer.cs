namespace WOWAutoFishing
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
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xStepTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.yStepTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.moveTimeTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.macroKeyTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.macroTimeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(64, 235);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "开始钓鱼";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(145, 235);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "停止钓鱼";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "设置快捷键(a-z)之间的字母";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(173, 16);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(100, 21);
            this.keyTextBox.TabIndex = 3;
            this.keyTextBox.Text = "t";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "横向步长";
            // 
            // xStepTextBox
            // 
            this.xStepTextBox.Location = new System.Drawing.Point(173, 45);
            this.xStepTextBox.Name = "xStepTextBox";
            this.xStepTextBox.Size = new System.Drawing.Size(100, 21);
            this.xStepTextBox.TabIndex = 5;
            this.xStepTextBox.Text = "15";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "纵向步长";
            // 
            // yStepTextBox
            // 
            this.yStepTextBox.Location = new System.Drawing.Point(173, 75);
            this.yStepTextBox.Name = "yStepTextBox";
            this.yStepTextBox.Size = new System.Drawing.Size(100, 21);
            this.yStepTextBox.TabIndex = 7;
            this.yStepTextBox.Text = "25";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "移动时间间隔";
            // 
            // moveTimeTextBox
            // 
            this.moveTimeTextBox.Location = new System.Drawing.Point(173, 107);
            this.moveTimeTextBox.Name = "moveTimeTextBox";
            this.moveTimeTextBox.Size = new System.Drawing.Size(100, 21);
            this.moveTimeTextBox.TabIndex = 9;
            this.moveTimeTextBox.Text = "15";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "其他宏快捷键";
            // 
            // macroKeyTextBox
            // 
            this.macroKeyTextBox.Location = new System.Drawing.Point(173, 157);
            this.macroKeyTextBox.Name = "macroKeyTextBox";
            this.macroKeyTextBox.Size = new System.Drawing.Size(100, 21);
            this.macroKeyTextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "使用宏后等待时间";
            // 
            // macroTimeTextBox
            // 
            this.macroTimeTextBox.Location = new System.Drawing.Point(173, 198);
            this.macroTimeTextBox.Name = "macroTimeTextBox";
            this.macroTimeTextBox.Size = new System.Drawing.Size(100, 21);
            this.macroTimeTextBox.TabIndex = 13;
            this.macroTimeTextBox.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 271);
            this.Controls.Add(this.macroTimeTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.macroKeyTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.moveTimeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.yStepTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xStepTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "怀旧服自动钓鱼";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xStepTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox yStepTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox moveTimeTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox macroKeyTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox macroTimeTextBox;
    }
}

