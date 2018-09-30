namespace 大龙的代码生成器
{
    partial class CodeGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeGenerator));
            this.label1 = new System.Windows.Forms.Label();
            this.txtConStr = new System.Windows.Forms.TextBox();
            this.btnCon = new System.Windows.Forms.Button();
            this.clbTabs = new System.Windows.Forms.CheckedListBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnChoose = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(81, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "连数据库";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtConStr
            // 
            this.txtConStr.Location = new System.Drawing.Point(86, 24);
            this.txtConStr.Name = "txtConStr";
            this.txtConStr.Size = new System.Drawing.Size(418, 21);
            this.txtConStr.TabIndex = 1;
            this.txtConStr.Text = "Data Source=X7GRHIT8276BLK3;Initial Catalog=RuPengSite;Integrated Security=True";
            // 
            // btnCon
            // 
            this.btnCon.BackColor = System.Drawing.Color.White;
            this.btnCon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCon.Location = new System.Drawing.Point(519, 22);
            this.btnCon.Name = "btnCon";
            this.btnCon.Size = new System.Drawing.Size(75, 23);
            this.btnCon.TabIndex = 2;
            this.btnCon.Text = "连接";
            this.btnCon.UseVisualStyleBackColor = false;
            this.btnCon.Click += new System.EventHandler(this.btnCon_Click);
            // 
            // clbTabs
            // 
            this.clbTabs.FormattingEnabled = true;
            this.clbTabs.Location = new System.Drawing.Point(86, 124);
            this.clbTabs.Name = "clbTabs";
            this.clbTabs.Size = new System.Drawing.Size(194, 212);
            this.clbTabs.TabIndex = 3;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.White;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Location = new System.Drawing.Point(519, 86);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.BackColor = System.Drawing.Color.White;
            this.btnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoose.Location = new System.Drawing.Point(519, 55);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 23);
            this.btnChoose.TabIndex = 5;
            this.btnChoose.Text = "选择";
            this.btnChoose.UseVisualStyleBackColor = false;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(86, 55);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(418, 21);
            this.txtPath.TabIndex = 6;
            this.txtPath.Text = "C:\\Users\\Administrator\\Desktop\\生成的三层代码";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "生成路径";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(8, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "命名空间";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(86, 87);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(134, 21);
            this.txtNameSpace.TabIndex = 9;
            this.txtNameSpace.Text = "RuPengSite";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(22, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "表名选择";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(216, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 23);
            this.label6.TabIndex = 15;
            this.label6.Text = "去掉前缀";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(364, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 23);
            this.label7.TabIndex = 16;
            this.label7.Text = "去掉后缀";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(302, 86);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(57, 21);
            this.txtPrefix.TabIndex = 17;
            this.txtPrefix.Text = "T_";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(447, 86);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(57, 21);
            this.txtSuffix.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::大龙的代码生成器.Properties.Resources.wings;
            this.pictureBox1.Location = new System.Drawing.Point(300, 124);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(294, 212);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // CodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(631, 361);
            this.Controls.Add(this.txtSuffix);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.clbTabs);
            this.Controls.Add(this.btnCon);
            this.Controls.Add(this.txtConStr);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CodeGenerator";
            this.Text = "代码生成器_V1.0";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConStr;
        private System.Windows.Forms.Button btnCon;
        private System.Windows.Forms.CheckedListBox clbTabs;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FolderBrowserDialog fbdPath;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TextBox txtSuffix;
    }
}

