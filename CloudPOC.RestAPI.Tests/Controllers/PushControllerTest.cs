using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudASPNETWebApi.Controllers;
using CloudASPNETWebApi.Models;
using System.Configuration;

namespace CloudASPNETWebApi.Tests.Controllers
{
    [TestClass]
    public class PushControllerTest
    {
        [TestMethod]
        public void Test_To_Validate_Number_Of_Urls_Pushed()
        {
            string message = DateTime.Now.ToString() +','+ DayOfWeek.Sunday.ToString();            
            PushController pushController = new PushController();
            int actualValue = pushController.PushMessages(message).Count;            
            Assert.AreEqual(2,actualValue);
        }

        [TestMethod]
        public void Test_To_Validate_Pushed_Urls()
        {
            string message = DateTime.Now.ToString() + ',' + DayOfWeek.Sunday.ToString();
            List<Push> pushActual = new List<Push>();
            PushController pushController = new PushController();
            pushActual = pushController.PushMessages(message);
            List<string> parsedMsg = new List<string>();
            Assert.AreEqual(parsedMsg[0], pushActual[0].cloudMessage);
            var delimiter = ConfigurationManager.AppSettings["Delimiter"];
            parsedMsg = message.Split(Convert.ToChar(delimiter)).ToList();
            Assert.AreEqual(parsedMsg[1], pushActual[1].cloudMessage);
        }

        [TestMethod]
        public void Test_To_Validate_Urls_Are_Pushed_To_Queue()
        {
            string message = DateTime.Now.ToString() + ',' + DayOfWeek.Sunday.ToString();
            List<Push> pushActual = new List<Push>();
            PushController pushController = new PushController();
            pushActual = pushController.PushMessages(message);           
            Assert.AreEqual(true, pushActual[0].result);
            Assert.AreEqual(true, pushActual[1].result);
        }

        [TestMethod]
        public void Test_To_Validate_Urls_Is_Null()
        {
            string message = null;
            List<Push> pushActual = new List<Push>();
            PushController pushController = new PushController();
            pushActual = pushController.PushMessages(message);
            Assert.AreEqual(0, pushActual.Count);            
        }

       

    }
}
