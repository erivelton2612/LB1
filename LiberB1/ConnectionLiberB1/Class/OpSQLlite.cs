using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using Utils.Encrypt;
using ConnectionLiberB1.Class;

namespace ConnectionLiberB1.Class
{
    class OpSQLlite
    {
        private SQLiteConnection myconn;
        String key = "trugLk";

        public OpSQLlite()
        {
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                myconn = new SQLiteConnection("DataSource=" + path + "\\LiberB1\\LiberB1DB.db;");
                myconn.Open();

            }
            catch (Exception ex)
            {
                //implementar aqui um create database para o caso do banco nao existir ainda.

                MessageBox.Show("Error 501 - " + ex.Message);
            }


        }

        internal string Getconnection(int field)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string myreader;

            sqlite_cmd = myconn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT [serverName],[dbName],[dbId],[dbPass],[SapUser],[SapPass]," +
                "    [ConexaoLiber],[userLiber],[PassLiber],[IsValid],[PortLiber]" +
                "from[Connection]; ";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            sqlite_datareader.Read();
            myreader = sqlite_datareader.GetString(field);
            return myreader;

        }

        internal string Getconfiguration(int field)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string myreader;

            sqlite_cmd = myconn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT [Id],[QueueName],[OriginDoc],[TimeSAP],[FieldChaveAcesso],[TimeStartSAP],[TimeStopSAP],[Import] " +
                "from[Configuration]; ";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            sqlite_datareader.Read();
            if (sqlite_datareader[0] == DBNull.Value) return "";

            if(field == 3 || field == 0)
            {
                myreader = sqlite_datareader.GetInt32(field).ToString();
            }
            else
            {
                //myreader = sqlite_datareader.GetString(field);
                myreader = Convert.ToString(sqlite_datareader[field]);
            }

            return myreader;

        }


        //set
        public void CreateConnection(string serverName, string dbName, string dbId, string dbPass, string SapUser, string SAPPass,
            string LiberEnd, string userLiber, string passLiber, string portLiber, string SQLtype)
        {
            

            if (ExistConn())
            {
                
                String sqlupdate = "UPDATE [Connection] SET [Id] = 1,[serverName] = '" + serverName +
                                 "',[dbName] = '" + dbName +
                                 "',[dbId] = '" + dbId +
                                 "',[dbPass] = '" + Cryptography.Encrypt(dbPass,key) +
                                 "',[SapUser] = '" + SapUser +
                                 "',[SapPass] = '" + Cryptography.Encrypt(SAPPass,key) +
                                 "',[ConexaoLiber] = '" + LiberEnd +
                                 "',[userLiber] = '" + userLiber +
                                 "',[PassLiber] = '" + Cryptography.Encrypt(passLiber,key) +
                                 "',[PortLiber] = '" + portLiber +
                                 "',[SQLType] = '" + SQLtype +
                                 "',[IsValid] = 1";
                try
                {
                    SQLiteCommand update = new SQLiteCommand(sqlupdate, myconn);
                    update.ExecuteNonQuery();
                    MessageBox.Show("Dados de Conexão Salvos!");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error 503 - " + ex.Message + "SQL: "+ sqlupdate);
                }

            }
            else
            {
                String sqlInsert = "INSERT INTO [Connection] ([Id],[serverName],[dbName],[dbId],[dbPass],[SapUser],[SapPass]," +
                                "[ConexaoLiber],[userLiber],[PassLiber],[PortLiber],[SQLType],[IsValid])"
                                + "VALUES (1,'" + serverName + "','"
                                            + dbName + "','"
                                            + dbId + "','"
                                            + Cryptography.Encrypt(dbPass,key) + "','"
                                            + SapUser + "','"
                                            + Cryptography.Encrypt(SAPPass,key) + "','"
                                            + LiberEnd + "','"
                                            + userLiber + "','"
                                            + Cryptography.Encrypt(passLiber, key) + "','"
                                            + portLiber + "','"
                                            + SQLtype + "',1);";

                try
                {
                    SQLiteCommand insert = new SQLiteCommand(sqlInsert, myconn);
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Dados de Conexão Salvos!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error 502 - " + ex.Message);
                }
            }

            
        }

        public bool ExistConn()
        {

            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            // create a new SQL command:
            try
            {
                sqlite_cmd = myconn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT ID  FROM[Connection]; ";

                // Now the SQLiteCommand object can give us a DataReader-Object:
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                if (sqlite_datareader.HasRows)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
 
        }

        public bool ExistConf()
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            // create a new SQL command:
            try
            {
                sqlite_cmd = myconn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT ID  FROM[Configuration]; ";

                // Now the SQLiteCommand object can give us a DataReader-Object:
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                if (sqlite_datareader.HasRows)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CloseConnection()
        {
            myconn.Close();
        }

        public void CreateConfiguration(String QueueName, String OriginDoc, int TimeSAP, String FieldKey,
                            String TimeStart, String TimeEnd, bool Import)
        {
            
            if (ExistConf())
            {

                String sqlupdate = "UPDATE [Configuration] SET [Id] = 1,[queuename] = '" + QueueName +
                                 "',[OriginDoc] = '" + OriginDoc +
                                 "',[TimeSAP] = " + TimeSAP +
                                 ",[TimeStartSAP] = '" + TimeStart +
                                 "',[TimeStopSAP] = '" + TimeEnd +
                                 "',[Import] = " + Import +
                                 ",[FieldChaveAcesso] = '" + FieldKey + "'";
                                
                try
                {
                    SQLiteCommand update = new SQLiteCommand(sqlupdate, myconn);
                    update.ExecuteNonQuery();
                    MessageBox.Show("Dados de Conexão Salvos!");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error 510 - " + ex.Message + "SQL: " + sqlupdate);
                }

            }
            else
            {
                String sqlInsert = "INSERT INTO [Configuration] ([queuename],[OriginDoc],[TimeSAP],[TimeStartSAP],[TimeStopSAP],[Import]" +
                    ",[FieldChaveAcesso]) "
                                + "VALUES ('" + QueueName + "',"
                                            //+ OriginDoc + ","
                                            + "null,"
                                            + TimeSAP.ToString() + ",'"
                                            + TimeStart + "','"
                                            + TimeEnd + "',"
                                            + Import + ",'"
                                            + FieldKey + "');";
                //MessageBox.Show(sqlInsert);
                
                try
                {
                    SQLiteCommand insert = new SQLiteCommand(sqlInsert, myconn);
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Configuralções Salvas!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error 507 - " + ex.Message);
                }
            }


        }

        public int GetSyncTime()
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            try
            {
                sqlite_cmd = myconn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT TimeSAP FROM [Configuration]; ";

                // Now the SQLiteCommand object can give us a DataReader-Object:
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                MessageBox.Show("Tempo:" + sqlite_datareader.GetInt32(0) * 60 * 1000 + " milissegundos!");
                
                return sqlite_datareader.GetInt32(0)*60*1000;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 1000000000;
            }

        }
    }
}
