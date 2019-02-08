using LiberB1Sync.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectionLiberB1;
using ConnectionLiberB1.Class;

namespace LiberB1Sync
{
    public partial class Sync : Form
    {
        Configuration cfg;
        Connection connSAPLiber;

        public Sync()
        {
            InitializeComponent();
            MyLogger.LogAdded += new EventHandler(MyLogger_LogAdded);

        }

        private void Sync_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipText = "Application minimized";
            notifyIcon1.BalloonTipTitle = "Liber B1 Sync";
            //verificar se há conexao
            OpSQLlite sQLlite = new OpSQLlite();

            //verifica se existe connexao
            connSAPLiber = new Connection();
            //if (!connSAPLiber.hasConn)
            if(!sQLlite.ExistConn())
            {
                button1.Enabled = false;
                button2.Enabled = false;
                toolStripStatusLabel3.BackColor = Color.Red;
                toolStripStatusLabel3.Text = "Sem Conexão.";
                label1TimeSAP.Text = "Temporizador não configurado";
                label1.Text = "";
                label2.Text = "";
                label3.Text = "";
            }
            else
            {
                toolStripStatusLabel3.BackColor = Color.Green;
                toolStripStatusLabel3.Text = "Conectado.";
                //verifica se existe configuração
                cfg = new Configuration();
                if (sQLlite.ExistConf())
                {
                    if (cfg.TimeSAP < 2) timer1.Enabled = false; else timer1.Interval = cfg.TimeSAP * 60 * 1000;

                    timer1.Start();
                    DateTime otherDate = DateTime.Now.AddMilliseconds(cfg.TimeSAP * 60 * 1000);
                    label1TimeSAP.Text = otherDate.ToLongTimeString();

                    DateTime otherDate2 = DateTime.Now.AddMilliseconds(cfg.TimeSAP * 60 * 1000);
                    label3.Text = otherDate2.ToLongTimeString();
                }
                else
                {
                    label1TimeSAP.Text = "Temporizador não configurado";
                    label3.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bool rtrn;
            //LiberRabbit lr = new LiberRabbit();
            //rtrn = lr.Connect();
            //textResult.Text = "Iniciando Sincronização..."+Environment.NewLine;
            button1.Enabled = false;
            toolStripStatusLabel1.BackColor = Color.Green;
            
            textResult.Refresh();
            new Operation();

            textResult.ScrollToCaret();
            button1.Enabled = true;
            textResult.Refresh();
            button1.Refresh();
            toolStripStatusLabel1.BackColor = Control.DefaultBackColor;
            this.Refresh();
        }

        void MyLogger_LogAdded(object sender, EventArgs e)
        {
            textResult.Text = textResult.Text + Environment.NewLine + MyLogger.GetLastLog();
        }


        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyLogger.LogAdded -= new EventHandler(MyLogger_LogAdded);
            this.Close();
        }

        private void conexãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConnectionLiberB1.Forms.Form1().Show();
        }

        private void configuraçõesGeraisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConnectionLiberB1.Forms.FormConfiguration().Show();
            this.Refresh();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Integrador LiberB1\nDesenvolvido por: Erivelton S Antonio\nRevisão: Liber Capital\nVersão:1.0\nOut/2018");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            //MessageBox.Show("Mensagem temporal");
            button1.Enabled = false;

            textResult.Refresh();
            new Operation();

            textResult.ScrollToCaret();
            button1.Enabled = true;
            textResult.Refresh();
            button1.Refresh();


            label1TimeSAP.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();

        }

        private void Sync_Resize(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Minimized)
            {
                ShowIcon = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
                ShowInTaskbar = false;
                ShowIcon = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel2.BackColor = Color.Green;
            new Request();
            toolStripStatusLabel2.BackColor = Control.DefaultBackColor;
            this.Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            new Request();
        }
    }
}
