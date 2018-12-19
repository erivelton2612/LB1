namespace ConnectionLiberB1.Forms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSAPUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSAPPass = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.labelConnSAP = new System.Windows.Forms.Label();
            this.textBoxDBID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxDBPass = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxPassLiber = new System.Windows.Forms.TextBox();
            this.labelConnLiber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxLiberPort = new System.Windows.Forms.TextBox();
            this.textBoxLiberUser = new System.Windows.Forms.TextBox();
            this.textBoxLiberEnd = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "ServerName";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(137, 34);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(182, 22);
            this.textBoxServerName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Company Name";
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(137, 68);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(182, 22);
            this.textBoxDBName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "SAPUser";
            // 
            // textBoxSAPUser
            // 
            this.textBoxSAPUser.Location = new System.Drawing.Point(137, 170);
            this.textBoxSAPUser.Name = "textBoxSAPUser";
            this.textBoxSAPUser.Size = new System.Drawing.Size(182, 22);
            this.textBoxSAPUser.TabIndex = 5;
            this.textBoxSAPUser.Text = "manager";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "SAPPassword";
            // 
            // textBoxSAPPass
            // 
            this.textBoxSAPPass.Location = new System.Drawing.Point(137, 204);
            this.textBoxSAPPass.Name = "textBoxSAPPass";
            this.textBoxSAPPass.PasswordChar = '*';
            this.textBoxSAPPass.Size = new System.Drawing.Size(182, 22);
            this.textBoxSAPPass.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.labelConnSAP);
            this.groupBox1.Controls.Add(this.textBoxDBID);
            this.groupBox1.Controls.Add(this.textBoxServerName);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBoxSAPPass);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBoxSAPUser);
            this.groupBox1.Controls.Add(this.textBoxDBPass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxDBName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(21, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 231);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conexão SAP";
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "dst_MSSQL2016";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "dst_MSSQL2008",
            "dst_MSSQL2012",
            "dst_MSSQL2014",
            "dst_MSSQL2016",
            "dst_MSSQL2017"});
            this.comboBox1.Location = new System.Drawing.Point(399, 67);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(138, 24);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.Text = "dst_MSSQL2016";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(331, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 17);
            this.label11.TabIndex = 9;
            this.label11.Text = "DB pass";
            // 
            // labelConnSAP
            // 
            this.labelConnSAP.AutoSize = true;
            this.labelConnSAP.Location = new System.Drawing.Point(346, 202);
            this.labelConnSAP.Name = "labelConnSAP";
            this.labelConnSAP.Size = new System.Drawing.Size(0, 17);
            this.labelConnSAP.TabIndex = 8;
            // 
            // textBoxDBID
            // 
            this.textBoxDBID.Location = new System.Drawing.Point(137, 102);
            this.textBoxDBID.Name = "textBoxDBID";
            this.textBoxDBID.Size = new System.Drawing.Size(182, 22);
            this.textBoxDBID.TabIndex = 3;
            this.textBoxDBID.Text = "sa";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(379, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 56);
            this.button1.TabIndex = 7;
            this.button1.Text = "Testar Conexão";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "DB ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "DB pass";
            // 
            // textBoxDBPass
            // 
            this.textBoxDBPass.Location = new System.Drawing.Point(137, 136);
            this.textBoxDBPass.Name = "textBoxDBPass";
            this.textBoxDBPass.PasswordChar = '*';
            this.textBoxDBPass.Size = new System.Drawing.Size(182, 22);
            this.textBoxDBPass.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxPassLiber);
            this.groupBox2.Controls.Add(this.labelConnLiber);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxLiberPort);
            this.groupBox2.Controls.Add(this.textBoxLiberUser);
            this.groupBox2.Controls.Add(this.textBoxLiberEnd);
            this.groupBox2.Location = new System.Drawing.Point(21, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(551, 150);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conexão Liber";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "URL";
            // 
            // textBoxPassLiber
            // 
            this.textBoxPassLiber.Location = new System.Drawing.Point(137, 120);
            this.textBoxPassLiber.Name = "textBoxPassLiber";
            this.textBoxPassLiber.PasswordChar = '*';
            this.textBoxPassLiber.Size = new System.Drawing.Size(182, 22);
            this.textBoxPassLiber.TabIndex = 10;
            // 
            // labelConnLiber
            // 
            this.labelConnLiber.AutoSize = true;
            this.labelConnLiber.Location = new System.Drawing.Point(346, 92);
            this.labelConnLiber.Name = "labelConnLiber";
            this.labelConnLiber.Size = new System.Drawing.Size(0, 17);
            this.labelConnLiber.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(379, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 56);
            this.button2.TabIndex = 11;
            this.button2.Text = "Testar Conexão";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "User";
            // 
            // textBoxLiberPort
            // 
            this.textBoxLiberPort.Location = new System.Drawing.Point(137, 54);
            this.textBoxLiberPort.Name = "textBoxLiberPort";
            this.textBoxLiberPort.Size = new System.Drawing.Size(65, 22);
            this.textBoxLiberPort.TabIndex = 9;
            this.textBoxLiberPort.Text = "5672";
            // 
            // textBoxLiberUser
            // 
            this.textBoxLiberUser.Location = new System.Drawing.Point(137, 87);
            this.textBoxLiberUser.Name = "textBoxLiberUser";
            this.textBoxLiberUser.Size = new System.Drawing.Size(182, 22);
            this.textBoxLiberUser.TabIndex = 10;
            // 
            // textBoxLiberEnd
            // 
            this.textBoxLiberEnd.Location = new System.Drawing.Point(137, 21);
            this.textBoxLiberEnd.Name = "textBoxLiberEnd";
            this.textBoxLiberEnd.Size = new System.Drawing.Size(222, 22);
            this.textBoxLiberEnd.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(412, 524);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(146, 33);
            this.button3.TabIndex = 13;
            this.button3.Text = "Fechar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(234, 524);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(146, 33);
            this.button4.TabIndex = 12;
            this.button4.Text = "Salvar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ConnectionLiberB1.Properties.Resources.b1_png;
            this.pictureBox2.Location = new System.Drawing.Point(389, 31);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(121, 72);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(48, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(165, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.configuraçõesToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(589, 28);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(109, 26);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            this.configuraçõesToolStripMenuItem.Click += new System.EventHandler(this.configuraçõesToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 569);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "LiberB1 Connection";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDBName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSAPUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSAPPass;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxPassLiber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxLiberUser;
        private System.Windows.Forms.TextBox textBoxLiberEnd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxDBID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxDBPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLiberPort;
        private System.Windows.Forms.Label labelConnSAP;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelConnLiber;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
    }
}