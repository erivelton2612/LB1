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

namespace LiberB1Sync
{
    public partial class Sync : Form
    {
        Configuration cfg;

        public Sync()
        {
            InitializeComponent();
            MyLogger.LogAdded += new EventHandler(MyLogger_LogAdded);
            MyLogger.Log("Aguardando iterações..." + Environment.NewLine);

        }

        private void Sync_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipText = "Application minimized";
            notifyIcon1.BalloonTipTitle = "Liber B1 Sync";
            cfg = new Configuration();
            if (cfg.TimeSAP < 2) timer1.Enabled = false; else timer1.Interval = cfg.TimeSAP * 60 * 1000;
            //MessageBox.Show(timer1.Interval.ToString() + "milissegundos!");
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
            toolStripStatusLabel1.BackColor = Control.DefaultBackColor;
            button1.Enabled = true;
            textResult.Refresh();
            button1.Refresh();
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

        private void button2_Click(object sender, EventArgs e)
        {
            new Request();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            new Request();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyLogger.LogAdded += new EventHandler(MyLogger_LogAdded);
        }
    }
}
