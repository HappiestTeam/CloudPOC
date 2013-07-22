using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudASPNETWebApi.Models;
using System.Configuration;
using CloudASPNETWebApi.Helper;
using System.Web.Http;
using Newtonsoft.Json;


namespace CloudASPNETWebApi.Controllers
{
    public class PushController : ApiController
    {       
        List<Push> lstPush = new List<Push>();
        Push push;
//Todo
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public List<Push> PushMessages(string msg)       
        {
            string queueName = ConfigurationManager.AppSettings["QueueName"];
            try
            {
                List<string> parsedMsg = Utility.parseMessage(msg);
                for (int count = 0; count < parsedMsg.Count; count++)
                {
                    push = new Push();
                    push.cloudMessage = parsedMsg[count];
                    push.result = Utility.pushMessageToQueue(parsedMsg[count], queueName);
                    push.cloudMesageId = count;
                    if (push.result)
                        Logger.logInformation(parsedMsg[count] + " message got inserted to " + queueName + " queue");
                    else
                        Logger.logInformation(parsedMsg[count] + " message is not inserted to " + queueName + " queue. Please check ExceptionLog file for more details.");
                    lstPush.Add(push);
                }
            }
            catch (Exception ex)
            {
                Logger.logException(ex.Message);
            }
            return lstPush;
        }
    }
}
