using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using Utils.Encrypt;
using ConnectionLiberB1.Class;
using System.IO;

namespace ConnectionLiberB1.Class
{
    public class OpSQLlite
    {
        private SQLiteConnection myconn;
        String key = "trugLk";

        public OpSQLlite()
        {

            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            try
            {
                if(System.IO.File.Exists(path + "\\LiberB1\\LiberB1DB.db"))
                {

                    myconn = new SQLiteConnection("DataSource=" + path + "\\LiberB1\\LiberB1DB.db;");
                    myconn.Open();
                }
                else
                {
                    MessageBox.Show("Mensagem 901 - Nenhuma Conexão configurada. Criar nova conexão.");
                    //implementar aqui um create database para o caso do banco nao existir ainda.
                    //criando nova conexão
                    CreateDatabase();
                }

            }
            catch (Exception ex )
            {

                MessageBox.Show("Mensagem 901 - "+ ex.Message);
                //implementar aqui um create database para o caso do banco nao existir ainda.
                //criando nova conexão

            }
        }

        private void CreateDatabase()
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            SQLiteConnection.CreateFile(path + "\\LiberB1\\LiberB1DB.db;");

            using (myconn = new SQLiteConnection("DataSource=" + path + "\\LiberB1\\LiberB1DB.db;"))
            {
                try
                {
                    myconn.Open();

                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT 1;");
                    SQLiteCommand command = new SQLiteCommand(varname1.ToString(), myconn);
                    command.ExecuteNonQuery();

                    StringBuilder varname11 = new StringBuilder();
                    varname11.Append("PRAGMA foreign_keys=OFF;");
                    command = new SQLiteCommand(varname11.ToString(), myconn);
                    command.ExecuteNonQuery();


                    StringBuilder varname12 = new StringBuilder();
                    varname12.Append("BEGIN TRANSACTION;");
                    command = new SQLiteCommand(varname12.ToString(), myconn);
                    command.ExecuteNonQuery();



                    StringBuilder varname13 = new StringBuilder();
                    varname13.Append("CREATE TABLE [Connection] ( \n");
                    varname13.Append("  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL \n");
                    varname13.Append(", [serverName] text NOT NULL \n");
                    varname13.Append(", [dbName] text NOT NULL \n");
                    varname13.Append(", [dbPass] text NOT NULL \n");
                    varname13.Append(", [SapUser] text NOT NULL \n");
                    varname13.Append(", [SapPass] text NOT NULL \n");
                    varname13.Append(", [IsValid] image DEFAULT 0 NOT NULL \n");
                    varname13.Append(", [ConexaoLiber] text DEFAULT 'https:\\\\staging.edi.libercapital.com.br' NOT NULL \n");
                    varname13.Append(", [userLiber] text NOT NULL \n");
                    varname13.Append(", [PassLiber] text NOT NULL \n");
                    varname13.Append(", [dbId] text DEFAULT sa NULL \n");
                    varname13.Append(", [PortLiber] text DEFAULT '5672' NOT NULL \n");
                    varname13.Append(", [SQLType] text DEFAULT dst_MSSQL2016 NOT NULL);");
                    command = new SQLiteCommand(varname13.ToString(), myconn);
                    command.ExecuteNonQuery();

                    StringBuilder varname22 = new StringBuilder();
                    varname22.Append("CREATE TABLE [Configuration] ( \n");
                    varname22.Append("  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL \n");
                    varname22.Append(", [QueueName] text NOT NULL \n");
                    varname22.Append(", [OriginDoc] text NULL \n");
                    varname22.Append(", [TimeSAP] bigint NULL \n");
                    varname22.Append(", [FieldChaveAcesso] text NOT NULL \n");
                    varname22.Append(", [TimeStartSAP] text NULL \n");
                    varname22.Append(", [TimeStopSAP] text NULL \n");
                    varname22.Append(", [Import] int DEFAULT false NOT NULL \n");
                    varname22.Append(");");
                    command = new SQLiteCommand(varname22.ToString(), myconn);
                    command.ExecuteNonQuery();

                    StringBuilder varname23 = new StringBuilder();
                    varname23.Append("CREATE TABLE [Access] ([Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL");
                    varname23.Append(", [LastTimeSAP] text NOT NULL, [LastTimeLiber] text NOT NULL);");
                    command = new SQLiteCommand(varname23.ToString(), myconn);
                    command.ExecuteNonQuery();

                    StringBuilder varname16 = new StringBuilder();
                    varname16.Append("COMMIT;");
                    command = new SQLiteCommand(varname16.ToString(), myconn);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro 5235 - Erro ao Criar base de dados - " + ex.Message);
                    
                }
               

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
            sqlite_datareader.Close();
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
            sqlite_datareader.Close();

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
                    MessageBox.Show("Error 510 - " + ex.Message);
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
                {
                    sqlite_datareader.Close();
                    return true;
                }
                else
                {
                    sqlite_datareader.Close();
                    return false;
                }
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
                {
                    sqlite_datareader.Close();
                    return true;
                }
                else
                {
                    sqlite_datareader.Close();
                    return false;
                }
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
                
                int x =  sqlite_datareader.GetInt32(0)*60*1000;

                sqlite_datareader.Close();

                return x;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 1000000000;
            }

        }
    }
}
