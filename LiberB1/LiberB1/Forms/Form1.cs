using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiberB1.Classes;

namespace LiberB1.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.connect();
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
    }
}
