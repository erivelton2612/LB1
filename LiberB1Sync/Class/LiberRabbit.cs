using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectionLiberB1.Class;
using RabbitMQ.Client;
using LiberB1Sync;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace LiberB1Sync.Class
{
    class LiberRabbit
    {

        private SQLiteConnection myconn;
        String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        String endpoint = null, LiberUser = null, LiberPass = null, extName = "casale", LiberPort=null;
        IConnection conn=null;

        public LiberRabbit()
        {

        }

        public bool Connect()
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            String key = "trugLk";

            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                myconn = new SQLiteConnection("DataSource=" + path + "\\LiberB1\\LiberB1DB.db;");
                myconn.Open();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error 501 - " + ex.Message);
            }


            sqlite_cmd = myconn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT [ConexaoLiber],[userLiber],[PassLiber],[PortLiber]" +
                "from[Connection]; ";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            sqlite_datareader.Read();

            endpoint = sqlite_datareader.GetString(0);
            LiberUser = sqlite_datareader.GetString(1);
            LiberPass = Cryptography.Decrypt(sqlite_datareader.GetString(2), key);
            LiberPort = sqlite_datareader.GetString(3);

            myconn.Close();
            //MessageBox.Show(endpoint + " ---- " + LiberUser + " ---- " + LiberPass + " ---- " + LiberPort);

            //testar a conexao com o rabbit
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = LiberUser,
                Password = LiberPass,
                VirtualHost = "/",
                Port = System.Convert.ToInt32(LiberPort),
                HostName = endpoint
            };

            try
            {
                conn = factory.CreateConnection();

                //IModel channel = conn.CreateModel();

                //MessageBox.Show("Conectado com sucesso!");

                //channel.Close();
                //conn.Close();

                //MyLogger.Log("Rabbit conectado");

                return true;
            }
            catch (Exception ex)
            {

                MyLogger.Log("Falha na conexão - " + ex);
                MessageBox.Show("Falha na conexão - " + ex);
                return false;
            }

        }

        public bool WriteJson(String json, String routingKey)
        {
            String queueName = extName + ".input";
            try
            {
                return WriteMessageOnQueue(json, queueName, conn, routingKey);
            }
            catch (Exception ex)
            {
                MyLogger.Log("Erro 505 - " + ex.Message);
                MessageBox.Show("Erro 505 - "+ex.Message);
                return false;
            }
        }

        public IConnection CreateConnection(ConnectionFactory connectionFactory)
        {
            return connectionFactory.CreateConnection();
        }

        public bool WriteMessageOnQueue(string message, string queueName, IConnection connection, String routingKey)
        {
            try
            {
                using (var channel = connection.CreateModel())
                {
                    ///trocar casale.input para queueName
                    //channel.ExchangeDeclare("casale.input", "fanout");
                    channel.BasicPublish(queueName, routingKey, null, Encoding.ASCII.GetBytes(message));
                }


                MyLogger.Log("Mensagem enviada à Liber com sucesso!");

                return true;
            }

            catch (Exception ex)
            {

                MyLogger.Log("Error 506 - " + ex.Message);
                MessageBox.Show("Error 506 - "+ex.Message);
                return false;
            }
           
        }

        //escuta a fila, recebe um json 
        public string ListenQueueRequest()
        {
            try
            {
                String queueName = extName + ".output";
                String json;

                IModel channel = conn.CreateModel();

                bool noAck = false;
                BasicGetResult result = channel.BasicGet(queueName, noAck);

                if (result == null)
                {
                    MyLogger.Log("No message available at this time.");

                    channel.Close();
                    return null;
                }
                else
                {
                    IBasicProperties props = result.BasicProperties;
                    byte[] body = result.Body;

                    json = Encoding.UTF8.GetString(body);
                    // acknowledge receipt of the message
                    MessageBox.Show(json);
                    channel.BasicAck(result.DeliveryTag, false);
                    MessageBox.Show(json);
                    MyLogger.Log("Uma mensagem lida");

                    channel.Close();
                    return json;
                }

            }
            catch (Exception ex)
            {
                MyLogger.Log("Erro 409 - Falha na conexao ou busca do titulo - " + ex.Message);
                MessageBox.Show("erro 409 - Falha na conexao ou busca do titulo - " + ex.Message);
                return String.Empty;
                    
            }
           
            //InvoiceRequest request = new InvoiceRequest("abc", "\"transID:166049,lineID:0\"",
            //    "920,47",
            // "BRL",
            // "2017-12-04");

            //request = JsonTextReader(new StringReader(json));
        }
    }
}
