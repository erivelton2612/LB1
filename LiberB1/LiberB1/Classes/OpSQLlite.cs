using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LiberB1.Classes
{
    class OpSQLlite
    {
        private SQLiteConnection myconn;

        public OpSQLlite()
        {
            string path = ".../";
            myconn = new SQLiteConnection("DataSource="+ path+"LiberB1DB.db;Version=3");
            myconn.Open();
        }

        //get
        public string GetServerName()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconn;
            return cmd.CommandText = "Select top 1 servername from Connection where isValid=0";

        }

        public string GetDbName()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconn;
            return cmd.CommandText = "Select top 1 dbName from Connection where isValid=0";

        }

        public string GetDbPass()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconn;
            return cmd.CommandText = "Select top 1 DbPass from Connection where isValid=0";
        }

        public string GetSAPUser()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconn;
            return cmd.CommandText = "Select top 1 SAPUser from Connection where isValid=0";
        }

        public string GetSAPPass()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconn;
            return cmd.CommandText = "Select top 1 SAPPass from Connection where isValid=0";
        }

        //set
        public void SetConnection(string serverName, string dbName, string dbPass, string SapUser, string SAPPass)
        {
            string strUpdate, strInsert;

            strUpdate = "UPDATE[Connection] " +
            "SET[Id] = [IsValid] = 1        " +
            "WHERE[IsValid] = 0;            ";


            strInsert = "INSERT INTO[Connection]   "
            + "        ,[serverName]          "
            + "        ,[dbName]             "
            + "        ,[dbPass]             "
            + "        ,[SapUser]            "
            + "        ,[SapPass]            "
            + "        ,[IsValid])           "
            + "  VALUES                      "
            + "        (" + serverName + ","
                         + dbName + ","
                         + dbPass + ","
                         + SapUser + ","
                         + SAPPass + ",1);";
        }

    }
}
