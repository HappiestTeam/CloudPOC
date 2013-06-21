using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace CloudASPNETWebApi.Helper
{
    public static class Utility
    {
        public static List<string> parseMessage(string msg)
        {
            List<string> parsedMsg = new List<string>();
            var delimiter = ConfigurationManager.AppSettings["Delimiter"];
            parsedMsg = msg.Split(Convert.ToChar(delimiter)).ToList();
            return parsedMsg;
        }

        public static bool pushMessageToQueue(string msg, string queueName)
        {
            try
            {
                var connectionFactory = new ConnectionFactory();
                IConnection connection = connectionFactory.CreateConnection();
                IModel channel = connection.CreateModel();
                channel.QueueDeclare(queueName, false, false, false, null);

                byte[] message = Encoding.UTF8.GetBytes(msg);
                channel.BasicPublish(string.Empty, queueName, null, message);

                channel.Close();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logger.logException(msg + " message is not inserted to " + queueName + " queue. Exception details: " + ex.Message);
            }

            return false;
        }
    }
}