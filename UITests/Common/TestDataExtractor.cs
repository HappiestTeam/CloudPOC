using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Collections;

namespace CloudPOC.Common
{
    public class TestDataExtractor
    {
        private string dataPath;

        public TestDataExtractor(string filePath)
        {
            dataPath = filePath;
 
        }

        public string GetTestDataValue(string testGroup, string testDataName)
        {

            if (!File.Exists(dataPath))
            {
                return null;
            }
            else
            {
                try
                {

                    XDocument doc = XDocument.Load(dataPath);
                    XElement root = doc.Root;
                    //XNamespace ns = "http://schemas.microsoft.com/VisualStudio/TeamTest/UITest/2010";
                    XNamespace ns = String.Empty;
                    return root.Descendants(ns + "TestGroup").Where(group => group.Attribute("Name").Value == testGroup).FirstOrDefault().Elements(ns + "TestData").Where(data => data.Attribute("Name").Value == testDataName).FirstOrDefault().Attribute(ns + "Value").Value;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }
    }
}
