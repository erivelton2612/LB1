using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiberB1Sync.Class
{
    class Configuration
    {

        private SQLiteConnection myconn;
        private string QueueName { get; set; }
        public String OriginDoc { get; set; }
        public String FieldChaveAcesso { get; set; }
        public String TimeStartSAP { get; set; }
        public String TimeStopSAP { get; set; }
        private int? timesap;
        private bool hascfg;
        public bool hasConfig { get; set; }

        private bool? import;
        
        public Configuration()
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;


            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                myconn = new SQLiteConnection("DataSource=" + path + "\\LiberB1\\LiberB1DB.db;");
                myconn.Open();
            }
            catch (Exception ex)
            {
                MyLogger.Log("Error 502 - " + ex.Message);
                MessageBox.Show("Error 502 - " + ex.Message);
                hascfg = false;
                return;
            }

            sqlite_cmd = myconn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT [Id],[QueueName],[OriginDoc],[TimeSAP],[FieldChaveAcesso],[TimeStartSAP]" +
                ",[TimeStopSAP],[Import] " +
                "FROM[Configuration];";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            sqlite_datareader.Read();

            if (sqlite_datareader.HasRows)
            {
                hascfg = true;
                QueueName = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("QueueName")) ?? "";
                if (sqlite_datareader.IsDBNull(sqlite_datareader.GetOrdinal("OriginDoc"))) 
                {
                    OriginDoc = string.Empty;
                }
                else
                {
                    OriginDoc = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("OriginDoc")) ?? "";
                }

                if (sqlite_datareader.IsDBNull(sqlite_datareader.GetOrdinal("TimeSAP")))
                {
                    timesap = -1;
                }
                else
                {
                    timesap = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("TimeSAP"));
                }

                if (sqlite_datareader.IsDBNull(sqlite_datareader.GetOrdinal("FieldChaveAcesso")))
                {
                    FieldChaveAcesso = string.Empty;
                }
                else { FieldChaveAcesso = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("FieldChaveAcesso")) ?? ""; }
                
                if (sqlite_datareader.IsDBNull(sqlite_datareader.GetOrdinal("TimeStartSAP")))
                {
                    TimeStartSAP = string.Empty;
                }
                else
                {
                    TimeStartSAP = sqlite_datareader.GetString(5) ?? "";
                }

                if (sqlite_datareader.IsDBNull(sqlite_datareader.GetOrdinal("TimeStopSAP")))
                {
                    TimeStopSAP = string.Empty;
                }
                else
                {
                    TimeStopSAP = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("TimeStopSAP")) ?? "";
                }


                if (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("Import")) == 0)
                {
                    import = false;
                }
                else
                {
                    import = true;
                }
            }
            else
            {
                hascfg = false;
            }

        }

        public bool HasTimeSAP { get { return timesap.HasValue; } }

        public int TimeSAP
        {
            get { return timesap.Value; }
            set { timesap = value; } 
        }

        public bool Import
        {
            get { return import.Value; }
            set { import = value; }
        }
        
        public bool HasConfig()
        {
            return hascfg;
        }
    }
}
