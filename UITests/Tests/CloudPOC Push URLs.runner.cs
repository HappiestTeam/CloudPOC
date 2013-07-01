using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UITests.Tests 
{
    [TestClass]
    public partial class CloudPOC_PushURLs 
    {

        
        [TestMethod]
        public void PushSingleURLToCloud()
        {         
            Given_the_CloudPOC_website_is_accessible_from_web_browser();        
            When_I_push_an__URL__to_cloud("URL");        
            Then_the_URL_shoud_be_listed_in_the_Response_result_with_status_as("status");
        }
        
        [TestMethod]
        public void PushMultipleURLsToCloud()
        {         
            Given_the_CloudPOC_website_is_accessible_from_web_browser();        
            When_I_push_multiple_URLS__to_cloud("URLs");        
            Then_the_URLs_shoud_be_listed_in_the_Response_result_with_status_as("status");
        }

    }
}
