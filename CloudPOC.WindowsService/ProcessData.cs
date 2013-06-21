using System;
using System.Text;
using Apache.Cassandra;
using Aquiles.Helpers;
using Aquiles.Helpers.Encoders;
using Aquiles.Core.Cluster;
using RabbitMQ.Client;
using System.Net;
using System.IO;

namespace Service.Scheduler
{
    public static class ProcessData
    {
        static string KEYSPACENAME = "las";
        static string queueName = "Demo-Queue";
        static string downloadFolderLocation = @"c:\temp";

        public static void PullDataFromQueue()
        {
            string url = string.Empty;
            var connectionFactory = new ConnectionFactory();
            IConnection connection = connectionFactory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.QueueDeclare(queueName, false, false, false, null);

            while (true)
            {
                BasicGetResult result = channel.BasicGet(queueName, true);
                if (result == null) continue;

                url = Encoding.UTF8.GetString(result.Body);
                if (!string.IsNullOrEmpty(url))
                    DownloadFile(url);
            }

            channel.Close();
            connection.Close();
        }

        private static void DownloadFile(string url)
        {
            string localFileName = string.Empty;
            FileInfo fileInfo = null;
            string tmpFileName = "tmpFile";
            Uri uri = new Uri(url);
            using(WebClient webClient = new WebClient())
            {
                localFileName = System.IO.Path.GetFileName(uri.LocalPath);
                webClient.DownloadFile(url, localFileName);
                fileInfo = new FileInfo(localFileName);
            }            

            InsertIntoCassandraDB(url, fileInfo);
        }

        private static void InsertIntoCassandraDB(string url, FileInfo fileInfo)
        {
            byte[] key = ByteEncoderHelper.UTF8Encoder.ToByteArray(url);
            byte[] extension = ByteEncoderHelper.UTF8Encoder.ToByteArray(fileInfo.Extension);
            byte[] size = ByteEncoderHelper.UTF8Encoder.ToByteArray(fileInfo.Length.ToString());

            ColumnParent columnParent = new ColumnParent();
            Column urlColumn = new Column()
            {
                Name = ByteEncoderHelper.UTF8Encoder.ToByteArray("url"),
                Timestamp = UnixHelper.UnixTimestamp,
                Value = key
            };

            Column extensionColumn = new Column()
            {
                Name = ByteEncoderHelper.UTF8Encoder.ToByteArray("extension"),
                Timestamp = UnixHelper.UnixTimestamp,
                Value = extension
            };

            Column sizeColumn = new Column()
            {
                Name = ByteEncoderHelper.UTF8Encoder.ToByteArray("size"),
                Timestamp = UnixHelper.UnixTimestamp,
                Value = size
            };

            columnParent.Column_family = "resource";

            ICluster cluster = Aquiles.Cassandra10.AquilesHelper.RetrieveCluster("Test Cluster");
            cluster.Execute(new Aquiles.Cassandra10.ExecutionBlock(delegate(Cassandra.Client client)
            {
                client.insert(key, columnParent, urlColumn, ConsistencyLevel.ONE);
                client.insert(key, columnParent, extensionColumn, ConsistencyLevel.ONE);
                client.insert(key, columnParent, sizeColumn, ConsistencyLevel.ONE);
                return null;
            }), KEYSPACENAME);
        }
    }
}
