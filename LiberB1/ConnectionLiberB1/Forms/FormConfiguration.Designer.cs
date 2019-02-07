namespace ConnectionLiberB1.Forms
{
    partial class FormConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfiguration));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTimeSAP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFieldKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxQueueName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTimeStart = new System.Windows.Forms.TextBox();
            this.textBoxTimeEnd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkImport = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxOriginDoc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Intervalo de Tempo para Conexão com o SAP:";
            // 
            // textBoxTimeSAP
            // 
            this.textBoxTimeSAP.Location = new System.Drawing.Point(318, 36);
            this.textBoxTimeSAP.Name = "textBoxTimeSAP";
            this.textBoxTimeSAP.Size = new System.Drawing.Size(82, 22);
            this.textBoxTimeSAP.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Campo chave de acesso na tabela da NFE:";
            // 
            // textBoxFieldKey
            // 
            this.textBoxFieldKey.Location = new System.Drawing.Point(302, 79);
            this.textBoxFieldKey.Name = "textBoxFieldKey";
            this.textBoxFieldKey.Size = new System.Drawing.Size(179, 22);
            this.textBoxFieldKey.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Nome da Fila:";
            // 
            // textBoxQueueName
            // 
            this.textBoxQueueName.Location = new System.Drawing.Point(128, 113);
            this.textBoxQueueName.Name = "textBoxQueueName";
            this.textBoxQueueName.Size = new System.Drawing.Size(128, 22);
            this.textBoxQueueName.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(253, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = ".input";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(221, 457);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(146, 33);
            this.button4.TabIndex = 11;
            this.button4.Text = "Salvar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(399, 457);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(146, 33);
            this.button3.TabIndex = 12;
            this.button3.Text = "Fechar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.checkImport);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxOriginDoc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.textBoxQueueName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxFieldKey);
            this.groupBox1.Controls.Add(this.textBoxTimeSAP);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 416);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurações";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxTimeEnd);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxTimeStart);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(18, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 74);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hora de Conexão";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(18, 167);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(221, 21);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Definir Intervalo para Conexão";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Início:";
            // 
            // textBoxTimeStart
            // 
            this.textBoxTimeStart.Location = new System.Drawing.Point(77, 37);
            this.textBoxTimeStart.Name = "textBoxTimeStart";
            this.textBoxTimeStart.Size = new System.Drawing.Size(95, 22);
            this.textBoxTimeStart.TabIndex = 7;
            // 
            // textBoxTimeEnd
            // 
            this.textBoxTimeEnd.Location = new System.Drawing.Point(287, 37);
            this.textBoxTimeEnd.Name = "textBoxTimeEnd";
            this.textBoxTimeEnd.Size = new System.Drawing.Size(95, 22);
            this.textBoxTimeEnd.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(237, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Fim";
            // 
            // checkImport
            // 
            this.checkImport.AutoSize = true;
            this.checkImport.Location = new System.Drawing.Point(18, 306);
            this.checkImport.Name = "checkImport";
            this.checkImport.Size = new System.Drawing.Size(148, 21);
            this.checkImport.TabIndex = 9;
            this.checkImport.Text = "Incluir Importações";
            this.checkImport.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(406, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "minutos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Origens de Documentos";
            // 
            // textBoxOriginDoc
            // 
            this.textBoxOriginDoc.Location = new System.Drawing.Point(186, 334);
            this.textBoxOriginDoc.Name = "textBoxOriginDoc";
            this.textBoxOriginDoc.Size = new System.Drawing.Size(169, 22);
            this.textBoxOriginDoc.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(361, 339);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "Separado por vírgulas";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(361, 356);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "Vazio para todos";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(406, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 17);
            this.label11.TabIndex = 11;
            this.label11.Text = "(recomendado: 60)";
            // 
            // FormConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 506);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormConfiguration";
            this.Text = "Configurações";
            this.Load += new System.EventHandler(this.FormConfiguration_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTimeSAP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFieldKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxQueueName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBoxTimeEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxTimeStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOriginDoc;
    }
}