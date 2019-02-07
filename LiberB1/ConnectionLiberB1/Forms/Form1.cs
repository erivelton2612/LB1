using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectionLiberB1.Class;
using RabbitMQ.Client;
using SAPbobsCOM;

namespace ConnectionLiberB1.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpSQLlite conn = new OpSQLlite();
            if (conn.ExistConn())
            {
                String key = "trugLk";
                textBoxServerName.Text = conn.Getconnection(0);
                textBoxDBName.Text = conn.Getconnection(1);
                textBoxDBID.Text = conn.Getconnection(2);
                textBoxDBPass.Text = Cryptography.Decrypt(conn.Getconnection(3),key);
                textBoxSAPUser.Text = conn.Getconnection(4);
                textBoxSAPPass.Text = Cryptography.Decrypt(conn.Getconnection(5),key);
                textBoxLiberEnd.Text = conn.Getconnection(6);
                textBoxLiberUser.Text = conn.Getconnection(7);
                textBoxPassLiber.Text = Cryptography.Decrypt(conn.Getconnection(8),key);
                textBoxLiberPort.Text = conn.Getconnection(10);
            }

            conn.CloseConnection();


        }

        private void connect()
        {
            //clsConectionSAP conn = new clsConectionSAP(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            //textBox9.Text =  conn.ConnStatus;
            //if(conn.RetCode != 0)
            //{
                OpSQLlite sql = new OpSQLlite();
            
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(labelConnLiber.ForeColor == Color.Green && labelConnSAP.ForeColor == Color.Green)
            {
                OpSQLlite conn = new OpSQLlite();
                conn.CreateConnection(textBoxServerName.Text, textBoxDBName.Text, textBoxDBID.Text, textBoxDBPass.Text,
                    textBoxSAPUser.Text, textBoxSAPPass.Text, textBoxLiberEnd.Text, textBoxLiberUser.Text, textBoxPassLiber.Text,
                    textBoxLiberPort.Text, comboBox1.Text);
                conn.CloseConnection();
                this.Close();
            }
            else
            {
                MessageBox.Show("Teste as Conexões SAP e Liber antes de Salvar.");
            }


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            labelConnSAP.Text = "Conectando...";
            labelConnSAP.ForeColor = Color.Gray;
            labelConnSAP.Refresh();

            SAPbobsCOM.Company oCompany = null;

            int retCode = -1;
            string strMsg;

            try
            {
                oCompany = new SAPbobsCOM.Company();
                oCompany.Server = textBoxServerName.Text;
                oCompany.CompanyDB = textBoxDBName.Text;
                switch (comboBox1.Text)
                {
                    case "dst_MSSQL": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL; break;
                    case "dst_DB_2": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_DB_2; break;
                    case "dst_SYBASE": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_SYBASE; break;
                    case "dst_MSSQL2005": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005; break;
                    case "dst_MAXDB": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MAXDB; break;
                    case "dst_MSSQL2008": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008; break;
                    case "dst_MSSQL2012": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012; break;
                    case "dst_MSSQL2014": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014; break;
                    case "dst_HANADB": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB; break;
                    case "dst_MSSQL2016": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016; break;
                    case "dst_MSSQL2017": oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017; break;

                    default:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017; ;
                        break;
                }
                //oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
                //dst_MSSQL;//1
                //dst_DB_2;//2
                //dst_SYBASE;//3
                //dst_MSSQL2005;//4
                //dst_MAXDB;//5
                //dst_MSSQL2008;//6
                //dst_MSSQL2012;//7
                //dst_MSSQL2014;//8
                //dst_HANADB;//9
                //dst_MSSQL2016;//10
                //dst_MSSQL2017;//11
                oCompany.UserName = textBoxSAPUser.Text;
                oCompany.Password = textBoxSAPPass.Text;
                oCompany.UseTrusted = true;

                retCode = oCompany.Connect();

                if (retCode != 0)
                {
                    strMsg = oCompany.GetLastErrorDescription();
                    labelConnSAP.Text = strMsg;
                    labelConnSAP.ForeColor = Color.Red;
                    MessageBox.Show(strMsg);
                }
                else
                {
                    labelConnSAP.Text = "Conectando a " + oCompany.CompanyName;
                    labelConnSAP.ForeColor = Color.Green;
                    button1.BackColor = Color.Green;

                    oCompany.Disconnect();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                labelConnSAP.Text = ex.Message;
                labelConnSAP.ForeColor = Color.Red;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //testar a conexao com o rabbit
            ConnectionFactory factory = new ConnectionFactory();

            factory.UserName = textBoxLiberUser.Text;
            factory.Password = textBoxPassLiber.Text;
            factory.VirtualHost = "/";
            factory.Port = System.Convert.ToInt32(textBoxLiberPort.Text);
            factory.HostName = textBoxLiberEnd.Text; ; factory.UserName = textBoxLiberUser.Text;

            labelConnLiber.Text = "Conectando...";
            labelConnLiber.ForeColor = Color.Gray;
            //factory.UserName = "liber-dev";
            //factory.Password = "trugLkvsZPjskaAu";
            //factory.Port = 5672;
            //factory.HostName = "edi.staging.libercapital.com.br";

            //String connect = "amqp://"+textBoxLiberUser+":"+textBoxPassLiber+"@"+textBoxLiberEnd+":5672/";

            try
            {
                IConnection conn = factory.CreateConnection();

                IModel channel = conn.CreateModel();

                labelConnLiber.Text = "Conectado com sucesso!";
                labelConnLiber.ForeColor = Color.Green;
                button2.BackColor = Color.Green;

                channel.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                labelConnLiber.Text = "Falha na conexão - " + ex;
                MessageBox.Show("Falha na conexão - " + ex);
                labelConnLiber.ForeColor = Color.Red;
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConnectionLiberB1.Forms.FormConfiguration().Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Integrador LiberB1\nDesenvolvido por: Erivelton S Antonio\nRevisão: Liber Capital\nVersão:1.0\nOut/2018");
        }
    }
}
