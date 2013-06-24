using System;
using System.Text;
using Apache.Cassandra;
using Aquiles.Helpers;
using Aquiles.Helpers.Encoders;
using Aquiles.Core.Cluster;
using RabbitMQ.Client;
using System.Net;
using System.IO;
using Thrift.Transport;
using Thrift.Protocol;

namespace Service.Scheduler
{
    public static class ProcessData
    {
        static string KEYSPACENAME = "las";
        static string queueName = "Demo-Queue";
        static string downloadFolderLocation = @"c:\temp";
        static bool fileDownloadSuccess;
        static bool success;

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
            if (!string.IsNullOrEmpty(url))
            {
                 uri = new Uri(url);
                fileDownloadSuccess = InitiateDownload(url, ref localFileName, ref fileInfo, uri);
                if (fileDownloadSuccess)
                {
                    InsertIntoCassandraDB(url, fileInfo);
                }
            }
        }

        private static bool InitiateDownload(string url, ref string localFileName, ref FileInfo fileInfo, Uri uri)
        {
            if(uri != null)
            {
            using (WebClient webClient = new WebClient())
            {
                localFileName = System.IO.Path.GetFileName(uri.LocalPath);
                CheckIfFileExists(url);
                webClient.DownloadFile(url, localFileName);
                fileInfo = new FileInfo(localFileName);
                if (fileInfo != null)
                {
                    fileDownloadSuccess = true;
                }
                else
                {
                    fileDownloadSuccess = false;
                }

                return fileDownloadSuccess;

            }
          }
            return fileDownloadSuccess = false;

        }

        private static bool CheckIfFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }

        }


        private static void InsertIntoCassandraDB(string url, FileInfo fileInfo)
        {
            CheckCassandraIsRunning();
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

        private static bool CheckCassandraIsRunning()
        {
            TTransport transport = new TSocket("localhost", 9160);
            TProtocol protocol = new TBinaryProtocol(transport);
            Cassandra.Client client = new Cassandra.Client(protocol);
            try
                {
                    transport.Open();
                    success = true;
                }
                catch
                {
                    success = false;
                }
              
            return success;
            
        }
    }
}
