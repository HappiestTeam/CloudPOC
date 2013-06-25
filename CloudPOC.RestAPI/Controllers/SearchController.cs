using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using Thrift;
using CassandraClient = Apache.Cassandra.Cassandra.Client;
using Apache.Cassandra;
using Aquiles.Helpers.Encoders;
using Aquiles.Cassandra10;
using Aquiles.Core;
using Aquiles.Core.Configuration;
using Aquiles.Core.Cluster;
using Aquiles.Helpers;

namespace CloudASPNETWebApi.Controllers
{
    public class SearchController : ApiController
    {
        const string KEYSPACENAME = "las";

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public bool Search(string url)
        {
            #region Unused code
            /*var doc = XDocument.Load(@"D:\\Search.xml");
            foreach (var child in doc.Descendants("cloudMessage"))
            {
                if (child.Value == msg)
                    return true;               
            }
            return false;
             */
            #endregion

            byte[] key = ByteEncoderHelper.UTF8Encoder.ToByteArray(url);
            //byte[] extension = ByteEncoderHelper.UTF8Encoder.ToByteArray("pdf");
            //byte[] size = ByteEncoderHelper.UTF8Encoder.ToByteArray("194329423");

            // Fetch inserted data
            ColumnPath columnPath = new ColumnPath()
            {
                Column = ByteEncoderHelper.UTF8Encoder.ToByteArray("url"),
                Column_family = "resource",
            };

            ColumnOrSuperColumn columnOrSuperColumn = null;

            #region Unused code
            /*
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

            cluster.Execute(new ExecutionBlock(delegate(CassandraClient client)
            {
                client.insert(key, columnParent, urlColumn, ConsistencyLevel.ONE);
                client.insert(key, columnParent, extensionColumn, ConsistencyLevel.ONE);
                client.insert(key, columnParent, sizeColumn, ConsistencyLevel.ONE);

                return null;
            }), KEYSPACENAME);
            */

            #endregion

            try
            {
                ICluster cluster = Aquiles.Cassandra10.AquilesHelper.RetrieveCluster("Test Cluster");
                cluster.Execute(new ExecutionBlock(delegate(CassandraClient client)
                {
                    columnOrSuperColumn = client.get(key, columnPath, ConsistencyLevel.ONE);
                    return columnOrSuperColumn;
                }), KEYSPACENAME);

                if (columnOrSuperColumn.Column.Value.SequenceEqual<byte>(key))
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}
