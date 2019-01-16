using ConnectionLiberB1.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionLiberB1.Forms
{
    public partial class FormConfiguration : Form
    {
        public FormConfiguration()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpSQLlite conn = new OpSQLlite();
            conn.CreateConfiguration(textBoxQueueName.Text, textBoxOriginDoc.Text,
                        System.Convert.ToInt32(textBoxTimeSAP.Text), textBoxFieldKey.Text,
                        textBoxTimeStart.Text, textBoxTimeEnd.Text, checkImport.Checked
                        );
        }

        private void FormConfiguration_Load(object sender, EventArgs e)
        {
            OpSQLlite conn = new OpSQLlite();
            if (conn.ExistConf())
            {
                textBoxOriginDoc.Text = conn.Getconfiguration(2);
                textBoxTimeSAP.Text = conn.Getconfiguration(3);
                textBoxFieldKey.Text = conn.Getconfiguration(4);
                textBoxQueueName.Text = conn.Getconfiguration(1);
                textBoxTimeStart.Text = conn.Getconfiguration(5);
                textBoxTimeEnd.Text = conn.Getconfiguration(6);
                if(string.IsNullOrEmpty(textBoxTimeStart.Text))
                {
                    checkBox1.Checked = false;
                }
                else
                {
                    textBoxTimeEnd.Text = "";
                }
                //if (String.Compare(conn.Getconfiguration(7)., "false"))
                //{

                //    checkImport.Checked = false;
                //}
                //else
                //{
                //    checkImport.Checked = true;

                //}

            }

            conn.CloseConnection();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox2.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = false;
            }
        }
    }
}
