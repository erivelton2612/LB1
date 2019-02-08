using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace LiberB1Sync.Class
{
    class Connection
    {

        private SQLiteConnection myconn;
        public bool hasConn { get; set; }

        public Connection()
        {

            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            try
            {

                String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                myconn = new SQLiteConnection("DataSource=" + path + "\\LiberB1\\LiberB1DB.db;");
                myconn.Open();

                sqlite_cmd = myconn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT [Id],[serverName],[dbName],[dbPass],[SapUser],[SapPass],[IsValid],[ConexaoLiber],[userLiber],[PassLiber],[dbId],[PortLiber],[SQLType] FROM [Connection];";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                sqlite_datareader.Read();

                if (sqlite_datareader.HasRows)
                {
                    hasConn = true;
                }
                else
                {
                    hasConn = false;
                }

                sqlite_datareader.Close();
                myconn.Close();
            }
            catch (Exception ex)
            {
                MyLogger.Log("Error 502 - " + ex.Message);
                MessageBox.Show("Error 502 - " + ex.Message);
                hasConn = false;
                myconn.Close();
                return;
            }
        }
    }
}
